using CasperDelivery.Data;
using CasperDelivery.Data.Models;
using CasperDelivery.Interfaces;
using CasperDelivery.Interfaces.Repositories;
using CasperDelivery.Services;
using Microsoft.EntityFrameworkCore;

namespace CasperDelivery.Repositories;

public class OrdersRepository : GenericRepository<OrderItem>, IOrdersRepository, IGenericRepository<OrderItem>
{
    private readonly AppDbContext _context;

    public OrdersRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<List<OrderItem>> GetOrdersByUserIdAsync(string userId)
    {
        return await _context.OrdersItems.Include(x => x.Order)
            .Include(x => x.Product).Where(x => x.Order.UserId == userId).ToListAsync();
    }
}