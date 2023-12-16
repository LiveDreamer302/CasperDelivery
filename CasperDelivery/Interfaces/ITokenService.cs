using CasperDelivery.Data.Models;

namespace CasperDelivery.Interfaces;

public interface ITokenService
{
    string CreateToken(AppUser user);
}