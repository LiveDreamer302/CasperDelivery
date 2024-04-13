using CasperDelivery.Data.Models;

namespace CasperDelivery.Interfaces.Repositories
{
    public interface IAddressRepository : IGenericRepository<Address>
    {
        Task<Address> GetAddressByUserId(string userId);
    }
}
