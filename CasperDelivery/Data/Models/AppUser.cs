using Microsoft.AspNetCore.Identity;

namespace CasperDelivery.Data.Models;

public class AppUser : IdentityUser
{
    public string DisplayName { get; set; }
    public Address Address { get; set; } = new Address();
}