using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurants_Platform.Dtos.Users;
using Restaurants_Platform.Errors;
using Restaurants_Platform.Interfaces;
using Restaurants_Platform.Mappers;

namespace Restaurants_Platform.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class AccountController : ControllerBase
{
    private readonly IAccountService _accountService;
    private readonly ITokenService _tokenService;

    public AccountController(IAccountService accountService, ITokenService tokenService)
    {
        _accountService = accountService;
        _tokenService = tokenService;
    }

    [HttpPost("Register"), AllowAnonymous]
    public async Task<IActionResult> Register(RegisterUserDto registerUser)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var user = registerUser.ToAppUserFromRegisterDto();

        var identityResult = await _accountService
            .CreateAppUserAsync(user, registerUser.Password);

        if (identityResult.Succeeded)
            return CreatedAtAction(nameof(Login),
                new { accessToken = _tokenService.CreateToken(user), user = user.ToAppUserDto()});
        
        return BadRequest(identityResult.Errors.Select(err => err.GetUserFriendlyErrorMessage()));
    }

    [HttpPost("Login"), AllowAnonymous]
    public async Task<IActionResult> Login(LoginDto loginDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _accountService.LoginAsync(loginDto.UserName, loginDto.Password);

        if (result.IsSuccess)
            return Ok(_tokenService.CreateToken(result.Value));
        
        return Unauthorized(result.Error.Message);
    }
}
