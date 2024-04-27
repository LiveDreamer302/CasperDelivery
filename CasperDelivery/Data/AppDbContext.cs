using CasperDelivery.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CasperDelivery.Data;

public class AppDbContext : IdentityDbContext<AppUser>
{

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Address> Address { get; set; }
    public DbSet<Orders> Orders { get; set; }
    public DbSet<Basket> Basket { get; set; }
    public DbSet<BasketItem> BasketItem { get; set; }
    public DbSet<OrderItem> OrdersItems { get; set; }
    public DbSet<Products> Products { get; set; }
    public DbSet<Restaurants> Restaurants { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<AppUser>()
            .HasOne(x => x.Address)
            .WithOne(x => x.User)
            .HasPrincipalKey<AppUser>(x => x.Id);
        builder.Entity<AppUser>()
            .HasOne(user => user.Basket)
            .WithOne(basket => basket.User)
            .HasForeignKey<Basket>(basket => basket.UserId);
        builder.Entity<Products>().Property(p => p.Price).HasColumnType("money");

        builder.Entity<BasketItem>()
            .HasOne(item => item.Basket)
            .WithMany(basket => basket.Items)
            .HasForeignKey(item => item.BasketId);

        builder.Entity<Basket>()
            .HasOne(basket => basket.User)
            .WithOne(user => user.Basket)
            .HasForeignKey<Basket>(basket => basket.UserId);

        builder.Entity<OrderItem>()
            .HasOne(item => item.Order)
            .WithMany(order => order.Items)
            .HasForeignKey(item => item.OrderId);

        builder.Entity<Orders>()
            .HasOne(order => order.User)
            .WithMany(user => user.Orders)
            .HasForeignKey(order => order.UserId);
        
        builder.Entity<Orders>().Property(p => p.TotalPrice).HasColumnType("money");
    
        


        base.OnModelCreating(builder);
    }
}