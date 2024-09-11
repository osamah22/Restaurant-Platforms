using Restaurants_Platform.DTOs.Orders.OrderItems;
using System.ComponentModel.DataAnnotations;

namespace Restaurants_Platform.DTOs.Orders;

public record CreateOrderRequestDto
{
    [Required]
    [MinLength(1, ErrorMessage = "At least one item is required")]
    public List<CreateOrderItemDto> Items { get; set; } = new List<CreateOrderItemDto>();
}