namespace Restaurants_Platform.DTOs.Orders.OrderItems;

// Used in CreateOrderDto
public class UpdateOrderItemDto
{
    public Guid FoodItemId { get; set; }
    public int Quantity { get; set; }
}
