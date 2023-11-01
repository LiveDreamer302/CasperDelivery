using System.Reflection;
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
    public DbSet<Products> Products { get; set; }
    public DbSet<Restaurants> Restaurants { get; set; }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<AppUser>()
            .HasOne(x => x.Address)
            .WithOne(x => x.User)
            .HasPrincipalKey<AppUser>(x => x.Id);

        base.OnModelCreating(builder);
    }
}