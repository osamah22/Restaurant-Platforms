using Restaurants_Platform.Dtos.Orders.OrderItems;
using Restaurants_Platform.Models;

namespace Restaurants_Platform.Mappers
{
    public static class OrderItemMappers
    {
        public static OrderItemDto ToOrderItemDto(this OrderItem orderItemModel)
        {
            /*          if (orderItemModel == null)
                          throw new ArgumentNullException(nameof(orderItemModel), "OrderItem cannot be null");

                      if (orderItemModel.FoodItem == null)
                          throw new ArgumentNullException(nameof(orderItemModel.FoodItem), "FoodItem cannot be null");

                      if (orderItemModel.FoodItem.Restaurant == null)
                          throw new ArgumentNullException(nameof(orderItemModel.FoodItem.Restaurant), "Restaurant cannot be null");*/
            decimal itemPrice = orderItemModel?.FoodItem?.Price ?? 0;
            var quantity = orderItemModel?.Quantity ?? 0;
            return new OrderItemDto
            {
                RestaurantId = orderItemModel.FoodItem.Restaurant.Id.ToString(),
                RestaurantName = orderItemModel.FoodItem.Restaurant.Name,
                FoodItemId = orderItemModel.FoodItemId.ToString(),
                FoodItemName = orderItemModel.FoodItem.Name,
                Quantity = quantity,
                Price = orderItemModel.FoodItem.Price,
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
}