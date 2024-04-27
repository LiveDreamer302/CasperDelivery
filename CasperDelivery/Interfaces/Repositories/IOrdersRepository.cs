using CasperDelivery.Data.Models;

namespace CasperDelivery.Interfaces.Repositories;

public interface IOrdersRepository
{
    Task<List<OrderItem>> GetOrdersByUserIdAsync(string userId);
}