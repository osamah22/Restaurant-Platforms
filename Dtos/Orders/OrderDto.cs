using Restaurants_Platform.Dtos.Orders.OrderItems;

namespace Restaurants_Platform.Dtos.Orders;

public record OrderDto(string Id, List<OrderItemDto> Itesm);
