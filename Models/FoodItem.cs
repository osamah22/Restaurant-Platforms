namespace Restaurants_Platform.Models;

public class FoodItem
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public Guid RestId { get; set; }
    public decimal Price { get; set; }
    public virtual Restaurant? Restaurant { get; set; }
}