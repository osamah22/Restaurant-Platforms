using Restaurants_Platform.Dtos.Orders.OrderItems;

namespace Restaurants_Platform.Dtos.Orders;

public class UpdateOrderRequestDto
{
    public Guid Id { get; set; }  // The ID of the order to be updated
    public List<UpdateOrderItemDto> Items { get; set; } = new List<UpdateOrderItemDto>();
}