using CasperDelivery.Data;
using CasperDelivery.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CasperDelivery.Controllers;

public class ProductController : BaseApiController
{
    private readonly AppDbContext _context;

    public ProductController(AppDbContext context)
    {
        _context = context;
    }
    
    [HttpGet("restaurants")]
    public async  Task<ActionResult<IReadOnlyList<Restaurants>>> GetRestaurants()
    {
        var restaurants = await _context.Restaurants.ToListAsync();

        return Ok(restaurants);
    }

    [HttpGet("restaurants/{id}")]
    public async Task<ActionResult<Restaurants>> GetRestaurantByIdAsync(int id)
    {
        var restaurant = await _context.Restaurants.FirstOrDefaultAsync(x => x.Id == id);

        return Ok(restaurant);
    }
}