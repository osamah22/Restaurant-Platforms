using System.ComponentModel.DataAnnotations;

namespace Restaurants_Platform.Dtos.FoodItems;

public record CreateFoodItemRequest
{
    [Required]
    public string Name { get; init; } = string.Empty;
    [Required]
    public Guid RestId { get; init; }
    [Required]
    [Range(1, 10000)]
    public decimal Price { get; init; }
}
