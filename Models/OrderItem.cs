namespace Restaurants_Platform.Models;

public class OrderItem
{
    // Primary Key = {OrderId, FoodItemId}
    public Guid OrderId { get; set; }
    public Guid FoodItemId { get; set; }

    public int Quantity { get; set; }

    public virtual FoodItem? FoodItem { get; set; }

    public virtual Order? Order { get; set; }
}
