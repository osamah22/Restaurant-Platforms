using System.ComponentModel.DataAnnotations;
namespace Restaurants_Platform.DTOs.Users;

public class LoginDto
{
    [Required]
    public string UserName { get; set; } = string.Empty;
    [Required]
    [MinLength(8)]
    public string Password { get; set; } = string.Empty;
}
