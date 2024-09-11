using Microsoft.AspNetCore.Identity;
using Restaurants_Platform.Shared;
using Restaurants_Platform.Interfaces;
using Restaurants_Platform.Models;
using Restaurants_Platform.Errors;

namespace Restaurants_Platform.Services;

public class AccountService : IAccountService
{
    private readonly UserManager<AppUser> _userManager;

    private readonly SignInManager<AppUser> _signInManager;

    public AccountService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task<bool> IsExitAsync(string username)
    {
        var user = await _userManager.FindByNameAsync(username);
        return user is not null;
    }

    public async Task<IdentityResult> CreateAppUserAsync(AppUser user, string password)
    {
        return await _userManager.CreateAsync(user, password);
    }

    public async Task<Result<AppUser>> LoginAsync(string userName, string password)
    {
        var user = await _userManager.FindByNameAsync(userName);

        if (user is null)
            return Result.Failure<AppUser>(ProcessErrors.LoginErrors.UserNotFound);

        if (!await _signInManager.CanSignInAsync(user))
            return Result.Failure<AppUser>(ProcessErrors.LoginErrors.AccountIsLockedOut);

        var result = await _signInManager.CheckPasswordSignInAsync(user, password, lockoutOnFailure: true);

        if (result.Succeeded)
            return Result.Success(user);

        else if (result.IsLockedOut)
            return Result.Failure<AppUser>(ProcessErrors.LoginErrors.AccountIsLockedOut);

        else if (result.IsNotAllowed)
            return Result.Failure<AppUser>(ProcessErrors.LoginErrors.SignInNotAllowed);

        throw new Exception("Unexpected error happedned while trying to login");
    }
}
