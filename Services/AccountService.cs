using Microsoft.AspNetCore.Identity;
using Restaurants_Platform.HelperClasses;
using Restaurants_Platform.Interfaces;
using Restaurants_Platform.Models;

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
            return Result<AppUser>.FailureResult("Username or password is not valid");

        if (!await _signInManager.CanSignInAsync(user))
            return Result<AppUser>.FailureResult("Account cannot sign in");

        var result = await _signInManager.CheckPasswordSignInAsync(user, password, lockoutOnFailure: true);

        if (result.Succeeded)
            return Result<AppUser>.SuccessResult(user, "Login successful");
        else if (result.IsLockedOut)
            return Result<AppUser>.FailureResult("Account is locked out");
        else if (result.IsNotAllowed)
            return Result<AppUser>.FailureResult("Sign-in not allowed");
        else
            return Result<AppUser>.FailureResult("Invalid login attempt");
    }
}
