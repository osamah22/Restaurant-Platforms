using Restaurants_Platform.Models;

namespace Restaurants_Platform.Dtos.Orders.OrderItems;

// Used in CreateOrderDto
public class UpdateOrderItemDto
{
    public Guid FoodItemId { get; set; }
    public int Quantity { get; set; }
}
