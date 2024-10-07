using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TechStoreFullRestAPI.Models;

[Table("categories")] // Define que esta clase representa la tabla "categories" en la base de datos.
public class Category
{
    [Key] // Indica que la propiedad "Id" es la clave primaria de la tabla.
    [Column("id")] // Especifica el nombre de la columna en la base de datos para este campo.
    public int Id { get; set; }

    [Required(ErrorMessage = "The name is required.")] // Indica que el campo "Name" es obligatorio. Si no se proporciona, se mostrará el mensaje de error especificado.
    [Column("name")] // Especifica el nombre de la columna en la base de datos para este campo.
    [MaxLength(255, ErrorMessage = "The name can't be longer than {1} characters.")] // El nombre no puede tener más de 255 caracteres.
    [MinLength(5, ErrorMessage = "The name can't be shorter than {1} character.")] // El nombre debe tener al menos 1 carácter.
    public string Name { get; set; }

    [Column("description")] // Define la columna "description" en la base de datos.
    [MaxLength(1000, ErrorMessage = "The description can't be longer than {1} characters.")] // La descripción no puede tener más de 1000 caracteres.
    [MinLength(10, ErrorMessage = "The description can't be shorter than {1} characters.")] // La descripción debe tener al menos 10 caracteres.
    public string Description { get; set; }
}
