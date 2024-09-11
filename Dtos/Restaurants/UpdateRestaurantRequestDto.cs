
using System.ComponentModel.DataAnnotations;

namespace Restaurants_Platform.DTOs.Restaurants;

public record UpdateRestaurantRequestDto
{
    [Required]
    public string Name { get; set; } = string.Empty;
    public string? Company { get; set; }
}
