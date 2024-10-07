using TechStoreFullRestAPI.Data;
using TechStoreFullRestAPI.Models;
using TechStoreFullRestAPI.Repositories;
using Microsoft.EntityFrameworkCore;
using TechStoreFullRestAPI.Enums;
using TechStoreFullRestAPI.DTOs.User;
using Microsoft.AspNetCore.Identity;

namespace TechStoreFullRestAPI.Services;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext Context;
    
    // PasswordHasher se utiliza para encriptar la contraseña del usuario antes de almacenarla.

    public UserRepository(ApplicationDbContext context)
    {
        Context = context;
    }

    // Método para verificar si un usuario existe por su ID
    public async Task<bool> CheckExistence(int id)
    {
        // Validar que el ID sea un número positivo
        if (id <= 0)
            throw new ArgumentException("El ID debe ser un número positivo.");

        // Buscar si existe un usuario con el ID proporcionado en la base de datos
        var user = await Context.Users.FindAsync(id);

        // Retornar true si el usuario existe, false si no
        return user != null;
    }

    // Método para obtener todos los usuarios
    public async Task<IEnumerable<User>> GetAllUsers()
    {
        // Obtener todos los usuarios de la base de datos
        var users = await Context.Users.ToListAsync();

        // Validar si la lista de usuarios está vacía
        if (users == null || users.Count == 0)
            throw new Exception("No se encontraron usuarios.");

        // Retornar la lista de usuarios
        return users;
    }

    // Método para obtener un usuario por su ID
    public async Task<User> GetUserById(int id)
    {
        // Validar que el ID sea un número positivo
        if (id <= 0)
            throw new ArgumentException("El ID debe ser un número positivo.");

        // Buscar el usuario en la base de datos por su ID
        var user = await Context.Users.FindAsync(id);

        // Si el usuario no existe, lanzar una excepción
        if (user == null)
            throw new Exception($"No se encontró un usuario con el ID {id}.");

        // Retornar el usuario encontrado
        return user;
    }

    // Método para obtener usuarios por su rol
    public async Task<IEnumerable<User>> GetUserByRole(Role role)
    {
        // Validar que el rol no sea nulo
        if (role == null)
            throw new ArgumentException("El rol no puede ser nulo.");

        // Filtrar usuarios que tienen el rol proporcionado
        var users = await Context.Users.Where(u => u.Role == role).ToListAsync();

        // Validar si se encontraron usuarios con ese rol
        if (users == null || users.Count == 0)
            throw new Exception($"No se encontraron usuarios con el rol {role}.");

        // Retornar la lista de usuarios con el rol especificado
        return users;
    }

    // Método para actualizar un usuario por su ID
    public async Task Update(int id, UserUpdateDTO userUpdateDTO)
    {
        // Validar que el ID sea positivo
        if (id <= 0)
            throw new ArgumentException("El ID debe ser un número positivo.");

        // Buscar el usuario en la base de datos por su ID
        var user = await Context.Users.FindAsync(id);

        // Si el usuario no existe, lanzar una excepción
        if (user == null)
            throw new Exception($"No se encontró un usuario con el ID {id}.");

        // Validar que el DTO de actualización no sea nulo
        if (userUpdateDTO == null)
            throw new ArgumentNullException(nameof(userUpdateDTO), "El DTO de actualización no puede ser nulo.");

        // Validar el campo de nombre (si se proporciona) y actualizarlo
        if (!string.IsNullOrEmpty(userUpdateDTO.Name))
        {
            user.Name = userUpdateDTO.Name;
        }

        // Validar el campo de apellido (si se proporciona) y actualizarlo
        if (!string.IsNullOrEmpty(userUpdateDTO.LastName))
        {
            user.LastName = userUpdateDTO.LastName;
        }

        // Validar el campo de email (si se proporciona) y actualizarlo
        if (!string.IsNullOrEmpty(userUpdateDTO.Email))
        {
            // Aquí podrías agregar validación extra para verificar si el formato del email es válido
            user.Email = userUpdateDTO.Email;
        }

        // Instancia el password hasher para encriptar la contraseña.
        var passwordHash = new PasswordHasher<User>();

        // Validar si se proporciona una nueva contraseña y hashearla
        if (!string.IsNullOrEmpty(userUpdateDTO.PasswordHash))
        {
            user.PasswordHash = passwordHash.HashPassword(user, userUpdateDTO.PasswordHash);
        }

        // Actualizar el rol si se proporciona
        if (userUpdateDTO.Role != null)
        {
            user.Role = userUpdateDTO.Role;
        }

        // Guardar los cambios en la base de datos
        Context.Users.Update(user);
        await Context.SaveChangesAsync();
    }

    // Método para eliminar un usuario por su ID
    public async Task Delete(int id)
    {
        // Validar que el ID sea positivo
        if (id <= 0)
            throw new ArgumentException("El ID debe ser un número positivo.");

        // Buscar el usuario en la base de datos
        var user = await Context.Users.FindAsync(id);

        // Si el usuario no existe, lanzar una excepción
        if (user == null)
            throw new Exception($"No se encontró un usuario con el ID {id}.");

        // Eliminar el usuario de la base de datos
        Context.Users.Remove(user);

        // Guardar los cambios
        await Context.SaveChangesAsync();
    }
}
