using CasperDelivery.Data;
using CasperDelivery.Data.Models;
using CasperDelivery.Interfaces.Repositories;
using CasperDelivery.Services;
using Microsoft.EntityFrameworkCore;

namespace CasperDelivery.Repositories
{
    public class AddressRepository : GenericRepository<Address>, IAddressRepository
    {
        private readonly AppDbContext _dbContext;

        public AddressRepository(AppDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        public Task<Address> GetAddressByUserId(string userId)
        {
            return _dbContext.Address.FirstOrDefaultAsync(x => x.User.Id == userId);
        }
        
    }

}
