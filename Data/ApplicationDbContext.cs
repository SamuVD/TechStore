using Microsoft.EntityFrameworkCore;
using TechStoreFullRestAPI.Models;

namespace TechStoreFullRestAPI.Data;

public class ApplicationDbContext : DbContext
{
    // Conjunto de entidades User que representa la tabla "users".
    public DbSet<User> Users { get; set; }

    // Conjunto de entidades Client que representa la tabla "clients".
    public DbSet<Client> Clients { get; set; }

    // Conjunto de entidades Product que representa la tabla "products".
    public DbSet<Product> Products { get; set; }

    // Conjunto de entidades Category que representa la tabla "categories".
    public DbSet<Category> Categories { get; set; }

    // Conjunto de entidades Order que representa la tabla "orders".
    public DbSet<Order> Orders { get; set; }

    // Conjunto de entidades OrderProduct que representa la tabla "orders_products".
    public DbSet<OrderProduct> OrderProducts { get; set; }

    // Constructor de la clase que recibe opciones de configuración.
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    // Método que se ejecuta al crear el modelo de la base de datos.
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder); // Llama al método base para permitir la configuración del modelo.

        // Definir relaciones muchos a muchos entre Order y Product.
        modelBuilder.Entity<OrderProduct>()
            .HasKey(op => new { op.OrderId, op.ProductId });  // Define la clave compuesta para OrderProduct.

        // Configura la relación entre OrderProduct y Order.
        modelBuilder.Entity<OrderProduct>()
            .HasOne(op => op.Order) // Cada OrderProduct tiene un Order.
            .WithMany(o => o.OrderProducts) // Un Order puede tener muchos OrderProducts.
            .HasForeignKey(op => op.OrderId); // Especifica que OrderId es la clave foránea.

        // Configura la relación entre OrderProduct y Product.
        modelBuilder.Entity<OrderProduct>()
            .HasOne(op => op.Product) // Cada OrderProduct tiene un Product.
            .WithMany(p => p.OrderProducts) // Un Product puede tener muchos OrderProducts.
            .HasForeignKey(op => op.ProductId); // Especifica que ProductId es la clave foránea.
    }
}
