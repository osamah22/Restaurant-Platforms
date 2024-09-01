using Restaurants_Platform.Models;

namespace Restaurants_Platform.Dtos.Orders.OrderItems;

// Used in CreateOrderDto
public class CreateOrderItemDto
{
    public Guid FoodItemId { get; set; }
    public int Quantity { get; set; }
}
