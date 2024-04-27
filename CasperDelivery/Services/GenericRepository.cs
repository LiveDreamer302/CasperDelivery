using CasperDelivery.Data;
using CasperDelivery.Data.Models;
using CasperDelivery.Interfaces;
using CasperDelivery.Interfaces.Specification;
using Microsoft.EntityFrameworkCore;

namespace CasperDelivery.Services;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{
    private readonly AppDbContext _context;

    public GenericRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<T>> GetAllAsync()
    {
        return await _context.Set<T>().ToListAsync();
    }

    public async Task<T> GetOneAsync(int id)
    {
        var item = await _context.Set<T>().FirstOrDefaultAsync(x => x.Id == id);
        return item;
    }


    public async Task DeleteAsync(int id)
    {
        var itemToRemove = await _context.Set<T>().FirstOrDefaultAsync(x => x.Id == id);
        _context.Set<T>().Remove(itemToRemove);
        await _context.SaveChangesAsync();
    }

    public async Task CreateAsync(T item)
    {
         await _context.Set<T>().AddAsync(item);
        await _context.SaveChangesAsync();
    }

    public async Task<T> GetEntityWithSpecAsync(ISpecification<T> spec)
    {
        return await ApplySpecification(spec).FirstOrDefaultAsync();
    }

    public async Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec)
    {
        return await ApplySpecification(spec).ToListAsync();
    }

    private IQueryable<T> ApplySpecification(ISpecification<T> spec)
    {
        return SpecificationEvaluator<T>.GetQuery(_context.Set<T>().AsQueryable(), spec);
    }

    public async Task UpdateAsync(T item)
    {
        _context.Set<T>().Update(item);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(List<T> items)
    {
        _context.Set<T>().UpdateRange(items);
        await _context.SaveChangesAsync();
    }
}
