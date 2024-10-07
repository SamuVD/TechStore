using System.ComponentModel.DataAnnotations;
using TechStoreFullRestAPI.Enums;

namespace TechStoreFullRestAPI.DTOs.User;

public class UserUpdateDTO
{
    [MaxLength(255, ErrorMessage = "The name can't be longer than {1} characters.")] // La longitud máxima del nombre es de 255 caracteres.
    [MinLength(1, ErrorMessage = "The name can't be shorter than {1} character.")] // El nombre debe tener al menos 1 carácter.
    public string Name { get; set; }

    [MaxLength(255, ErrorMessage = "The last name can't be longer than {1} characters.")] // La longitud máxima del apellido es de 255 caracteres.
    [MinLength(1, ErrorMessage = "The last name can't be shorter than {1} character.")] // El apellido debe tener al menos 1 carácter.
    public string LastName { get; set; }

    [MaxLength(255, ErrorMessage = "The email can't be longer than {1} characters.")] // La longitud máxima del correo electrónico es de 255 caracteres.
    [MinLength(1, ErrorMessage = "The email can't be shorter than {1} character.")] // El correo electrónico debe tener al menos 1 carácter.
    [EmailAddress(ErrorMessage = "You must write a correct email format.")] // Valida que el formato del correo electrónico sea correcto.
    public string Email { get; set; }

    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,}$", ErrorMessage = "Password must be at least 8 characters long and contain at least one uppercase letter, one lowercase letter, and one number.")] // Valida que la contraseña tenga al menos 8 caracteres, contenga al menos una letra mayúscula, una letra minúscula y un número.
    public string PasswordHash { get; set; }

    public Role Role { get; set; } // El tipo de dato es un Enum que está ubicado en la carpeta Enums, entonces hace referencia a que ese campo será manejado como un Enum.
}
