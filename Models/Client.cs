using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TechStoreFullRestAPI.Models;

[Table("clients")] // Define que esta clase representa la tabla "clients" en la base de datos.
public class Client
{  
    [Key] // Indica que la propiedad "Id" es la clave primaria de la tabla.
    [Column("id")] // Especifica el nombre de la columna en la base de datos para este campo.
    public int Id { get; set; }

    [Required(ErrorMessage = "The name is required.")] // Indica que el campo "Name" es obligatorio.
    [Column("name")] // Especifica el nombre de la columna en la base de datos para este campo.
    [MaxLength(255, ErrorMessage = "The name can't be longer than {1} characters.")] // El nombre no puede tener más de 255 caracteres.
    [MinLength(1, ErrorMessage = "The name can't be shorter than {1} character.")] // El nombre debe tener al menos 1 caracter.
    public string Name { get; set; }

    [Column("address")] // Especifica el nombre de la columna en la base de datos para este campo.
    [MaxLength(255, ErrorMessage = "The email can't be longer than {1} characters.")] // La longitud máxima de la dirección es de 255 caracteres.
    [MinLength(1, ErrorMessage = "The address can't be shorter than {1} character.")] // La longitud mínima de la dirección es de 1 caracter.
    public string Address { get; set; }

    [Column("phone_number")] // Especifica el nombre de la columna en la base de datos para este campo.
    [MaxLength(20, ErrorMessage = "The phone number can't be longer than {1} characters.")] // La longitud máxima del número de teléfono es de 20 caracteres.
    [MinLength(7, ErrorMessage = "The phone number can't be shorter than {1} characters.")] // La longitud mínima del número de teléfono es de 7 caracteres.
    public string PhoneNumber { get; set; }

    [Required(ErrorMessage = "The email is required.")] // Indica que el campo "Email" es obligatorio.
    [Column("email")] // Especifica el nombre de la columna en la base de datos para este campo.
    [MaxLength(255, ErrorMessage = "The email can't be longer than {1} characters.")] // La longitud máxima del correo electrónico es de 255 caracteres.
    [MinLength(1, ErrorMessage = "The email can't be shorter than {1} character.")] // La longitud mínima del correo electrónico es de 1 caracter.
    [EmailAddress(ErrorMessage = "You must write a correct email format.")] // Valida que el formato del correo electrónico sea correcto.
    public string Email { get; set; }
}
