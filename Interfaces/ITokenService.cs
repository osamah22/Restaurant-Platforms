using Restaurants_Platform.Models;

namespace Restaurants_Platform.Interfaces;

public interface ITokenService
{
    public string CreateToken (AppUser appUser);
}
