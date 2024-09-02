namespace Restaurants_Platform.Models;

public class Restaurant
{
    public Guid Id { get; set; }

    public required string Name { get; set; }

    public string? Company { get; set; }

    public virtual List<FoodItem>? Menu {get; set; }
}
