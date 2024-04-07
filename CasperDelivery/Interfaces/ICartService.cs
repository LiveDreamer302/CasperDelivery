using CasperDelivery.Data.Models;

namespace CasperDelivery.Interfaces
{
    public interface ICartService
    {
        Task AddItemToCart(int id);
        Task<Restaurants> GetRestaurantInfo(int id);
    }
}
