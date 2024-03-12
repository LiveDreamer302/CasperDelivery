using EllipticCurve.Utils;
using Microsoft.AspNetCore.Identity;

namespace CasperDelivery.Data.Models;

public class AppUser : IdentityUser
{
    public List<Orders> Orders { get; set; } = new List<Orders>();
    public Basket Basket { get; set; } = new Basket();
    public Address Address { get; set; } = new Address();
}