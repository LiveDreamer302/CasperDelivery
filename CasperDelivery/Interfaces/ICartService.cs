using CasperDelivery.Data.Models;

namespace CasperDelivery.Interfaces
{
    public interface ICartService
    {
        Task AddItemToCart(int id);
        Task DeleteOneItemFromCart(int id);
        Task<Restaurants> GetRestaurantInfo(int id);
        Task<string> Checkout(int basketId, string userId);
    }
}
