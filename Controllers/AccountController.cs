using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Restaurants_Platform.Dtos.Users;
using Restaurants_Platform.Interfaces;
using Restaurants_Platform.Mappers;

namespace Restaurants_Platform.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly IAccountService _accountService;
    private readonly ITokenService _tokenService;

    public AccountController(IAccountService accountService, ITokenService tokenService)
    {
        _accountService = accountService;
        _tokenService = tokenService;
    }

    [HttpPost("Register")]
    public async Task<IActionResult> Register(RegisterUserDto registerUser)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        var user = registerUser.ToAppUserFromRegisterDto();
        var identityResult = await _accountService
            .CreateAppUserAsync(user, registerUser.Password);

        if (identityResult.Succeeded)
            return Ok(_tokenService.CreateToken(user));
        else
            return BadRequest(identityResult.Errors);
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login(LoginDto loginDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _accountService.LoginAsync(loginDto.UserName, loginDto.Password);

        if (result.Success)
            return Ok(new { accessToken = _tokenService.CreateToken(result.Data!) });

        return BadRequest(result.Message);
    }
}
