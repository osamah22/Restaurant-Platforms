using Restaurants_Platform.DTOs.Orders.OrderItems;
using System.ComponentModel.DataAnnotations;

namespace Restaurants_Platform.DTOs.Orders;

public class UpdateOrderRequestDto
{
    public Guid Id { get; set; }  // The ID of the order to be updated
    [Range(1, 4, ErrorMessage = "Order Status is not valid")]
    public int StatusId { get; set; }
    public List<UpdateOrderItemDto> Items { get; set; } = new List<UpdateOrderItemDto>();
}