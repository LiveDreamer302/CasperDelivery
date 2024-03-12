using CasperDelivery.Data;
using CasperDelivery.Data.Models;
using CasperDelivery.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CasperDelivery.Services;

public class NavBarService
{
    private readonly AppDbContext _context;

    public NavBarService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Restaurants>> GetAllAsync()
    {
        return await _context.Restaurants.ToListAsync();
    }

    public async Task<Basket> GetBasketAsync(string id)
    {
        return await _context.Basket.FirstOrDefaultAsync(x => x.UserId == id);
    }
}