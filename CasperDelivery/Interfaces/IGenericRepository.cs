using CasperDelivery.Data.Models;
using CasperDelivery.Interfaces.Specification;

namespace CasperDelivery.Interfaces;

public interface IGenericRepository<T> where T : BaseEntity
{
    Task<List<T>> GetAllAsync();
    Task<T> GetOneAsync(int id);
    Task<T> GetEntityWithSpecAsync(ISpecification<T> spec);
    Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec);
    Task DeleteAsync(int id);
    Task CreateAsync(T item);
    Task UpdateAsync(T item);
    Task UpdateAsync(List<T> items);
}