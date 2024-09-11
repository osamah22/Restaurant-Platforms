using System.ComponentModel.DataAnnotations;

namespace Restaurants_Platform.DTOs;

public class CreateReviewDto
{
    public string? Content { get; set; }

    [Range(0, 5)] // 1-5 stars
    public int Star { get; set; }

    public Guid UserId { get; set; }

    public Guid RestaurantId { get; set; }
}
