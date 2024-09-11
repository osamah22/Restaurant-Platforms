using Restaurants_Platform.DTOs.Orders.OrderItems;
using Restaurants_Platform.Models;

namespace Restaurants_Platform.Mappers;

public static class OrderItemMappers
{
    public static OrderItemDto ToOrderItemDto(this OrderItem orderItemModel)
    {
        decimal itemPrice = orderItemModel?.FoodItem?.Price ?? 0;
        var quantity = orderItemModel?.Quantity ?? 0;

        return new OrderItemDto
        {
            RestaurantId = orderItemModel!.FoodItem?.Restaurant?.Id.ToString() ?? "Not availble",
            RestaurantName = orderItemModel.FoodItem?.Restaurant?.Name ?? "Not availble",
            FoodItemId = orderItemModel.FoodItemId.ToString(),
            FoodItemName = orderItemModel.FoodItem?.Name ?? "Not availble",
            Quantity = quantity,
            Price = orderItemModel.FoodItem?.Price ?? 0m,
            Total = itemPrice * quantity
        };
    }

    public static OrderItem ToOrderItemFromCreate(this CreateOrderItemDto itemDto)
    {
        if (itemDto == null)
            throw new ArgumentNullException(nameof(itemDto), "CreateOrderItemDto cannot be null");

        return new OrderItem
        {
            FoodItemId = itemDto.FoodItemId,
            Quantity = itemDto.Quantity
        };
    }
}