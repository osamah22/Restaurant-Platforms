using Restaurants_Platform.Models;

namespace Restaurants_Platform.DTOs.Reviews;

public class ReviewDto
{
    public string UserName { get; set; } = string.Empty;

    public string? Content { get; set; }

    public int Rate { get; set; } //[1 to 5 stars]

    public DateTime CreatedAt { get; set; }
}
