using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TechStoreFullRestAPI.Enums;

namespace TechStoreFullRestAPI.Models;

[Table("orders")] // Define que esta clase representa la tabla "orders" en la base de datos.
public class Order
{
    [Key] // Indica que la propiedad "Id" es la clave primaria de la tabla.
    [Column("id")] // Especifica el nombre de la columna en la base de datos para este campo.
    public int Id { get; set; } // Propiedad que representa el ID de la orden.

    [Required(ErrorMessage = "The status is required.")] // Indica que el campo "Status" es obligatorio.
    [Column("status")] // Especifica el nombre de la columna en la base de datos para este campo.
    public Status Status { get; set; } // Propiedad que representa el estado de la orden.

    [Required(ErrorMessage = "The order date is required.")] // Indica que el campo "OrderDate" es obligatorio.
    [Column("order_date")] // Especifica el nombre de la columna en la base de datos para este campo.
    [DataType(DataType.Date)] // Especifica que este campo debe ser tratado como una fecha (sin hora).
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")] // Define el formato a utilizar al mostrar la fecha.
    public DateTime OrderDate { get; set; } // Propiedad que representa la fecha de la orden.

    [Required(ErrorMessage = "The client id is required.")] // Indica que el campo "ClientId" es obligatorio.
    [Column("id_client")] // Especifica el nombre de la columna en la base de datos para este campo.
    public int ClientId { get; set; } // Propiedad que representa el ID del cliente asociado a la orden.

    [ForeignKey("ClientId")] // Indica que "ClientId" es la clave foránea que se relaciona con la tabla "Client".
    public Client Client { get; set; } // Propiedad de navegación que representa el cliente asociado a la orden.

    public List<OrderProduct> OrderProducts { get; set; } // Lista de productos asociados a la orden.
}

