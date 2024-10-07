using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TechStoreFullRestAPI.Models;

[Table("products")] // Especifica el nombre de la columna en la base de datos que almacenará este campo.
public class Product
{
    [Key] // Indica que la propiedad "Id" es la clave primaria en la base de datos.
    [Column("id")] // Especifica el nombre de la columna en la base de datos que almacenará este campo.
    public int Id { get; set; }

    [Required(ErrorMessage = "The name is required.")] // Indica que el campo "Name" es obligatorio. Si no se proporciona, se mostrará el mensaje de error especificado.
    [Column("name")] // Especifica el nombre de la columna en la base de datos para este campo.
    [MaxLength(255, ErrorMessage = "The name can't be longer than {1} characters.")] // El nombre no puede tener más de 255 caracteres.
    [MinLength(1, ErrorMessage = "The name can't be shorter than {1} character.")] // El nombre debe tener al menos 1 carácter.
    public string Name { get; set; }

    [Column("description")] // Define la columna "description" en la base de datos.
    [MaxLength(1000, ErrorMessage = "The description can't be longer than {1} characters.")] // La descripción no puede tener más de 1000 caracteres.
    [MinLength(10, ErrorMessage = "The description can't be shorter than {1} characters.")] // La descripción debe tener al menos 10 caracteres.
    public string Description { get; set; }

    [Required(ErrorMessage = "The price is required.")] // El campo "Price" es obligatorio.
    [Column("price")] // Especifica la columna "price" en la base de datos.
    [Range(0.1, 10000.0, ErrorMessage = "The price must be between {1} and {2}. And must be written in USD format.")] // El precio debe estar entre 0.1 y 10,000 en formato USD.
    [DisplayFormat(DataFormatString = "{0:F2}")] // Formato de visualización con dos decimales.
    public decimal Price { get; set; }

    [Required(ErrorMessage = "The stock quantity is required.")] // El campo "StockQuantity" es obligatorio.
    [Column("stock_quantity")] // Especifica la columna "stock_quantity" en la base de datos.
    [Range(1, 1000, ErrorMessage = "The stock quantity must be between {1} and {2}.")] // La cantidad de stock debe estar entre 1 y 1000 unidades.
    public int StockQuantity { get; set; }

    [Required(ErrorMessage = "The category is required.")] // El campo "IdCategory" es obligatorio.
    [Column("id_category")] // Especifica la columna "id_category" en la base de datos.
    public int IdCategory { get; set; }

    [ForeignKey("IdCategory")] // Indica que "IdCategory" es una clave foránea que hace referencia a la clase "Category".
    public Category Category { get; set; } // Relación con la entidad "Category", representando la categoría del producto.

    public List<OrderProduct> OrderProducts { get; set; } // Relación de uno a muchos con la clase "OrderProduct", indicando los productos en las órdenes.
}
