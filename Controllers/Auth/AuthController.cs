using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using TechStoreFullRestAPI.DTOs.User;
using TechStoreFullRestAPI.Models;
using TechStoreFullRestAPI.Data;
using TechStoreFullRestAPI.Config;

namespace TechStoreFullRestAPI.Controllers.Auth;

[ApiController]
[Route("api/v1/auth")] // Define la ruta base para el controlador de autenticación.
public class AuthController : ControllerBase
{
    protected readonly ApplicationDbContext Context; // Contexto de la base de datos inyectado.

    public readonly IConfiguration _configuration;
    // IConfiguration permite acceder a configuraciones como claves, conexiones de base de datos o JWT.

    public readonly PasswordHasher<User> _passwordHasher;
    // PasswordHasher se utiliza para encriptar la contraseña del usuario antes de almacenarla.

    // Constructor que inicializa el contexto de la base de datos, configuración y el password hasher.
    public AuthController(ApplicationDbContext context, IConfiguration configuration)
    {
        Context = context; // Inyecta el contexto de la base de datos.
        _configuration = configuration; // Inyecta la configuración.
        _passwordHasher = new PasswordHasher<User>(); // Inicializa el password hasher.
    }

    /// <summary>
    /// Registra un nuevo usuario en el sistema.
    /// </summary>
    /// <param name="registerDTO">Datos del usuario a registrar.</param>
    /// <returns>Retorna un estado Ok si el registro es exitoso, o un error si falla.</returns>
    /// <response code="200">Usuario registrado exitosamente.</response>
    /// <response code="400">El modelo es inválido.</response>
    /// <response code="409">El correo electrónico ya está en uso.</response>
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDTO registerDTO)
    {
        // Verifica si el modelo recibido en la solicitud es válido.
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState); // Si no es válido, retorna un error 400 Bad Request.
        }

        // Verifica si el correo electrónico ya existe en la base de datos.
        var existingEmail = await Context.Users.FirstOrDefaultAsync(user => user.Email == registerDTO.Email);

        if (existingEmail != null)
        {
            return Conflict("El correo electrónico ya está en uso. Por favor, elige otro."); // Retorna 409 si el correo está duplicado.
        }

        // Crea un nuevo objeto User con los datos recibidos en el DTO.
        var user = new User
        {
            Name = registerDTO.Name,
            LastName = registerDTO.LastName,
            Email = registerDTO.Email,
            PasswordHash = registerDTO.PasswordHash,
            Role = registerDTO.Role
        };

        // Instancia el password hasher para encriptar la contraseña.
        var passwordHash = new PasswordHasher<User>();

        // Encripta la contraseña del usuario antes de almacenarla en la base de datos.
        user.PasswordHash = passwordHash.HashPassword(user, registerDTO.PasswordHash);

        // Añade el nuevo usuario a la base de datos.
        Context.Users.Add(user);

        // Guarda los cambios en la base de datos de forma asíncrona.
        await Context.SaveChangesAsync();

        return Ok("Usuario registrado exitosamente.");
    }

    /// <summary>
    /// Inicia sesión en el sistema.
    /// </summary>
    /// <param name="loginDTO">Datos de inicio de sesión.</param>
    /// <returns>Retorna un token JWT si el inicio de sesión es exitoso, o un error si falla.</returns>
    /// <response code="200">Inicio de sesión exitoso con token JWT generado.</response>
    /// <response code="400">El modelo es inválido.</response>
    /// <response code="401">Credenciales inválidas.</response>
    [HttpPost("login")]
    [Authorize(Policy = "Admin")] // Solo Admins pueden acceder
    public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState); // Retorna error 400 si el modelo no es válido.
        }

        // Busca el usuario por correo electrónico.
        var user = await Context.Users.FirstOrDefaultAsync(item => item.Email == loginDTO.Email);
        if (user == null)
        {
            return Unauthorized("Credenciales inválidas."); // Retorna error 401 si no se encuentra el usuario.
        }

        // Verifica si la contraseña proporcionada coincide con la almacenada.
        var passwordResult = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, loginDTO.PasswordHash);
        if (passwordResult == PasswordVerificationResult.Failed)
        {
            return Unauthorized("Credenciales inválidas."); // Retorna error 401 si la contraseña es incorrecta.
        }

        // Si la autenticación es exitosa, genera un token JWT para el usuario.
        var token = JWT.GenerateJwtToken(user);

        // Retorna una respuesta OK con los datos del usuario y el token JWT.
        return Ok(new
        {
            id = user.Id,
            name = user.Name,
            lastName = user.LastName,
            email = user.Email,
            passwordHash = user.PasswordHash,
            role = user.Role,
            token = token
        });
    }
}
