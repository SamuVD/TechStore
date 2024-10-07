using System.ComponentModel.DataAnnotations;

namespace TechStoreFullRestAPI.DTOs.User;

public class LoginDTO
{
    [Required(ErrorMessage = "The email is required.")] // Indica que el campo "Email" es obligatorio.
    [MaxLength(255, ErrorMessage = "The email can't be longer than {1} characters.")] // La longitud máxima del correo electrónico es de 255 caracteres.
    [MinLength(1, ErrorMessage = "The email can't be shorter than {1} character.")] // El correo electrónico debe tener al menos 1 carácter.
    [EmailAddress(ErrorMessage = "You must write a correct email format.")] // Valida que el formato del correo electrónico sea correcto.
    public string Email { get; set; }

    [Required(ErrorMessage = "The password is required.")] // Indica que el campo "PasswordHash" es obligatorio.
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,}$", ErrorMessage = "Password must be at least 8 characters long and contain at least one uppercase letter, one lowercase letter, and one number.")] // Valida que la contraseña tenga al menos 8 caracteres, contenga al menos una letra mayúscula, una letra minúscula y un número.
    public string PasswordHash { get; set; }
}
