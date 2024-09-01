using System.ComponentModel.DataAnnotations;

namespace Restaurants_Platform.Dtos.FoodItems;

public class UpdateFoodItemRequestDto
{
    [Required]
    public string Name { get; set; } = string.Empty;
    [Required]
    [Range(1, 10000)]
    public decimal Price { get; init; }
}
