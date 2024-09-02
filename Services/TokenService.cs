using Microsoft.IdentityModel.Tokens;
using Restaurants_Platform.Interfaces;
using Restaurants_Platform.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Restaurants_Platform.Services;

public class TokenService : ITokenService
{
    private readonly IConfiguration _configuration;

    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string CreateToken(AppUser appUser)
    {
        if (appUser is null)
            throw new ArgumentNullException("AppUser cannot be null");

        // Define the claims that will be included in the token
        var claims = new[]
        {
                new Claim(JwtRegisteredClaimNames.Sub, appUser.UserName!),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, appUser.Id.ToString())
        };

        // Get the signing key from configuration
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        // Create the JWT token
        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddHours(1),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
