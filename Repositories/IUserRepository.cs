using TechStoreFullRestAPI.Models;
using TechStoreFullRestAPI.Enums;
using TechStoreFullRestAPI.DTOs.User;

namespace TechStoreFullRestAPI.Repositories;

public interface IUserRepository
{
    // Obtiene todos los usuarios de la base de datos.
    Task<IEnumerable<User>> GetAllUsers();

    // Obtiene un usuario por su ID.
    Task<User> GetUserById(int id);

    // Obtiene un usuario según su rol.
    Task<IEnumerable<User>> GetUserByRole(Role role);

    // Actualiza un usuario existente identificado por su ID.
    Task Update(int id, UserUpdateDTO userUpdateDTO);

    // Elimina un usuario de la base de datos por su ID.
    Task Delete(int id);

    // Verifica si un usuario existe en la base de datos por su ID.
    Task<bool> CheckExistence(int id);
}
