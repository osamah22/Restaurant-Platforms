namespace Restaurants_Platform.Dtos.Orders.OrderItems;

public record OrderItemDto
{
    public string RestaurantName { get; set; } = string.Empty;
    public string FoodItemName { get; set; } = string.Empty;
    public string RestaurantId { get; set; } = string.Empty;
    public string FoodItemId { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public decimal Total { get; set; }
}