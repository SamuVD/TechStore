using Microsoft.EntityFrameworkCore;
using DotNetEnv;
using TechStoreFullRestAPI.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Cargar variables de entorno desde un archivo .env.
Env.Load();

// Agregar variables de entorno al sistema de configuración del proyecto.
builder.Configuration.AddEnvironmentVariables();

// Obtener las variables de entorno para conectar a la base de datos.
var dbHost = Environment.GetEnvironmentVariable("DB_HOST");
var dbPort = Environment.GetEnvironmentVariable("DB_PORT");
var dbDatabaseName = Environment.GetEnvironmentVariable("DB_DATABASE");
var dbUser = Environment.GetEnvironmentVariable("DB_USERNAME");
var dbPassword = Environment.GetEnvironmentVariable("DB_PASSWORD");

// Construir la cadena de conexión a MySQL usando las variables de entorno.
var mySqlConnection = $"server={dbHost};port={dbPort};database={dbDatabaseName};uid={dbUser};password={dbPassword}";

// Registrar el contexto de base de datos (DbContext) en los servicios del proyecto.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(mySqlConnection, ServerVersion.Parse("8.0.20-mysql")));

// Obtener las variables de entorno necesarias para configurar la autenticación JWT.
var jwtKey = Environment.GetEnvironmentVariable("JWT_KEY");
var jwtIssuer = Environment.GetEnvironmentVariable("JWT_ISSUER");
var jwtAudience = Environment.GetEnvironmentVariable("JWT_AUDIENCE");
var jwtExpireMinutes = Environment.GetEnvironmentVariable("JWT_EXPIREMINUTES");

// Configurar la autenticación JWT en los servicios del proyecto.
// Se especifica que se utilizará el esquema de autenticación JWT para autenticar y desafiar las solicitudes.
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})

// Configuración específica del JWT, que incluye validaciones como el emisor, audiencia, tiempo de vida del token y la clave de seguridad utilizada para firmar los tokens.
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = true;  // HTTPS es obligatorio para el token.
    options.SaveToken = true;  // Guardar el token de autenticación.
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,  // Validar que el emisor del token es correcto.
        ValidateAudience = true,  // Validar que la audiencia del token es correcta.
        ValidateLifetime = true,  // Validar que el token no ha expirado.
        ValidateIssuerSigningKey = true,  // Validar que la clave de firma del token es válida.
        ValidIssuer = jwtIssuer,   // Definir el emisor válido.
        ValidAudience = jwtAudience,  // Definir la audiencia válida.
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))  // Definir la clave de firma del token.
    };
});

// Agregar el servicio de autorización, que se utilizará para restringir el acceso a ciertos endpoints.
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin", policy => policy.RequireRole(UserRole.Admin.ToString()));
    options.AddPolicy("Employee", policy => policy.RequireRole(UserRole.Employee.ToString()));
});

// Configurar políticas de CORS (Cross-Origin Resource Sharing).
// Esto permite que solo ciertos orígenes (dominios) hagan solicitudes a nuestra API.
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", // Nombre de la política de CORS
        builder =>
        {
            // Permitir cualquier tipo de encabezado y método HTTP (GET, POST, etc.).
            builder.WithOrigins("http://127.0.0.1:5173", "http://localhost:5173", "https://appnemura.netlify.app")
                   .AllowAnyHeader()
                   .AllowAnyMethod()
                   .AllowCredentials(); // Permitir el envío de credenciales (cookies, encabezados de autenticación).
        });
});

// Agregar soporte para controladores (MVC o API Controllers).
// Esto permite a la aplicación manejar solicitudes HTTP y devolver respuestas usando controladores.
builder.Services.AddControllers();

// Configurar Swagger, una herramienta que genera documentación interactiva para APIs.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "TechStore", Version = "v1" });
});

var app = builder.Build();

// Aquí se configura Swagger en la tubería de la aplicación, permitiendo a los usuarios ver la documentación de la API.
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "TechStore v1");
});

// Configurar CORS para que las políticas previamente definidas se apliquen a las solicitudes.
app.UseCors("AllowSpecificOrigin");

// Habilitar la autenticación en la tubería de la aplicación, lo que significa que cada solicitud pasará por el proceso de autenticación JWT.
app.UseAuthentication();

// Habilitar la autorización, que se utilizará para asegurar que solo los usuarios con permisos accedan a ciertos recursos.
app.UseAuthorization();

// Mapear los controladores definidos en la API para que respondan a las rutas HTTP.
app.MapControllers();

// Ejecutar la aplicación, iniciando el servidor y esperando solicitudes.
app.Run();
