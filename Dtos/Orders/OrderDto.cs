using Restaurants_Platform.DTOs.Orders.OrderItems;

namespace Restaurants_Platform.DTOs.Orders;

public record OrderDto(string Id, List<OrderItemDto> Itesm);
