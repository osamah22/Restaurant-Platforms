using Microsoft.AspNetCore.Identity;

namespace Restaurants_Platform.Models;

public class AppUser : IdentityUser<Guid>
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public List<Order> Orders { get; set; } = new List<Order>();
}
