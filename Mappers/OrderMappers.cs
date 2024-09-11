using Restaurants_Platform.DTOs.Orders;
using Restaurants_Platform.Models;

namespace Restaurants_Platform.Mappers;

public static class OrderMappers
{
    public static OrderDto ToOrderDto(this Order orderModel)
    {
        if (orderModel == null)
            throw new ArgumentNullException(nameof(orderModel), "Order cannot be null");

        if (orderModel.OrderItems == null)
            throw new ArgumentNullException(nameof(orderModel.OrderItems), "OrderItems cannot be null");

        var orderItemsDto = orderModel.OrderItems
            .Select(i => i.ToOrderItemDto())
            .ToList();

        return new OrderDto(orderModel.Id.ToString(), orderItemsDto);
    }
}