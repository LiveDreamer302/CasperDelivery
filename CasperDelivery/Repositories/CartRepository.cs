using CasperDelivery.Data;
using CasperDelivery.Data.Models;
using CasperDelivery.Interfaces.Repositories;
using CasperDelivery.Services;
using Microsoft.EntityFrameworkCore;

namespace CasperDelivery.Repositories
{
    public class CartRepository : GenericRepository<Basket>, ICartRepository
    {
        private readonly AppDbContext _appDbContext;

        public CartRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public Task<Basket> GetBasketByUserId(string userId)
        {
            return _appDbContext.Basket.FirstOrDefaultAsync(x => x.UserId == userId);
        }
        
    }
}
