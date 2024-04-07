using CasperDelivery.Data.Models;

namespace CasperDelivery.Interfaces.Repositories
{
    public interface ICartRepository : IGenericRepository<Basket>
    {
        Task<Basket> GetBasketByUserId(string userId);
    }
}
