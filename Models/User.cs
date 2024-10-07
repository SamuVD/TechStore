using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TechStoreFullRestAPI.Enums;

namespace TechStoreFullRestAPI.Models;

[Table("users")] // Define que esta clase representa la tabla "users" en la base de datos.
public class User
{
    [Key] // Indica que la propiedad "Id" es la clave primaria de la tabla.
    [Column("id")] // Especifica el nombre de la columna en la base de datos para este campo.
    public int Id { get; set; }

    [Required] // Indica que el campo "Name" es obligatorio.
    [Column("name")] // Especifica el nombre de la columna en la base de datos para este campo.
    [MaxLength(255, ErrorMessage = "The name can't be longer than {1} characters.")] // La longitud máxima del nombre es de 255 caracteres.
    [MinLength(1, ErrorMessage = "The name can't be shorter than {1} character.")] // El nombre debe tener al menos 1 carácter.
    public string Name { get; set; }

    [Required(ErrorMessage = "The last name is required.")] // Indica que el campo "LastName" es obligatorio.
    [Column("last_name")] // Especifica el nombre de la columna en la base de datos para este campo.
    [MaxLength(255, ErrorMessage = "The last name can't be longer than {1} characters.")] // La longitud máxima del apellido es de 255 caracteres.
    [MinLength(1, ErrorMessage = "The last name can't be shorter than {1} character.")] // El apellido debe tener al menos 1 carácter.
    public string LastName { get; set; }

    [Required(ErrorMessage = "The email is required.")] // Indica que el campo "Email" es obligatorio.
    [Column("email")] // Especifica el nombre de la columna en la base de datos para este campo.
    [MaxLength(255, ErrorMessage = "The email can't be longer than {1} characters.")] // La longitud máxima del correo electrónico es de 255 caracteres.
    [MinLength(1, ErrorMessage = "The email can't be shorter than {1} character.")] // El correo electrónico debe tener al menos 1 carácter.
    [EmailAddress(ErrorMessage = "You must write a correct email format.")] // Valida que el formato del correo electrónico sea correcto.
    public string Email { get; set; }

    [Required(ErrorMessage = "The password is required.")] // Indica que el campo "PasswordHash" es obligatorio.
    [Column("password_hash")] // Especifica el nombre de la columna en la base de datos para este campo.
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,}$", ErrorMessage = "Password must be at least 8 characters long and contain at least one uppercase letter, one lowercase letter, and one number.")] // Valida que la contraseña tenga al menos 8 caracteres, contenga al menos una letra mayúscula, una letra minúscula y un número.
    public string PasswordHash { get; set; }

    [Required(ErrorMessage = "The role is required.")] // Indica que el campo "Role" es obligatorio.
    [Column("role")] // Especifica el nombre de la columna en la base de datos para este campo.
    public Role Role { get; set; } // El tipo de dato es un Enum que está ubicado en la carpeta Enums, entonces hace referencia a que ese campo será manejado como un Enum.
}
