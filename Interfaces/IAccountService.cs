using Microsoft.AspNetCore.Identity;
using Restaurants_Platform.Helpers;
using Restaurants_Platform.Models;

namespace Restaurants_Platform.Interfaces;

public interface IAccountService
{
    public Task<IdentityResult> CreateAppUserAsync(AppUser user, string password);

    public Task<bool> IsExitAsync(string username);

    public Task<Result<AppUser>> LoginAsync(string userName, string password);
}
