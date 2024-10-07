using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TechStoreFullRestAPI.Models;

[Table("orders_products")] // Define que esta clase representa la tabla "orders_products" en la base de datos.
public class OrderProduct
{
    [Required(ErrorMessage = "The order id is required.")] // Indica que el campo "IdOrder" es obligatorio.
    [Column("id_order")] // Especifica el nombre de la columna en la base de datos para este campo.
    public int OrderId { get; set; } // Propiedad que representa el ID de la orden.

    [Required(ErrorMessage = "The product id is required.")] // Indica que el campo "IdProduct" es obligatorio.
    [Column("id_product")] // Especifica el nombre de la columna en la base de datos para este campo.
    public int ProductId { get; set; } // Propiedad que representa el ID del producto.

    [Required(ErrorMessage = "The quantity is required.")] // Indica que el campo "Quantity" es obligatorio.
    [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than {1}.")] // Valida que la cantidad sea mayor que 1.
    public int Quantity { get; set; } // Propiedad que representa la cantidad del producto en la orden.

    public Order Order { get; set; } // Propiedad de navegación que representa la orden asociada.
    public Product Product { get; set; } // Propiedad de navegación que representa el producto asociado.
}
