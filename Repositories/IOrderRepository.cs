// using TechStoreFullRestAPI.Models;
// using TechStoreFullRestAPI.Enums;

// namespace TechStoreFullRestAPI.Repositories;

// public interface IOrderRepository
// {
//     // Obtiene todas las órdenes de la base de datos.
//     IEnumerable<Order> GetAllOrders();

//     // Obtiene una orden por su ID.
//     Order GetOrderById(int id);

//     // Obtiene una orden según el ID del cliente asociado.
//     Order GetOrderByClientId(int id);

//     // Obtiene una orden según su estado.
//     Order GetOrderByStatus(Status status);

//     // Obtiene una orden según la fecha de la orden.
//     Order GetOrderByDate(OrderDate orderDate);

//     // Crea una nueva orden a partir de un DTO (Data Transfer Object).
//     Task<Order> Create(OrderCreateDTO orderCreateDTO);

//     // Actualiza una orden existente identificada por su ID.
//     Task<Order> Update(int id, OrderUpdateDTO orderUpdateDTO);

//     // Elimina una orden de la base de datos por su ID.
//     Task<Order> Delete(int id);

//     // Verifica si una orden existe en la base de datos por su ID.
//     Task<bool> CheckExistence(int id);
// }
