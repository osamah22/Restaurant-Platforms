using Restaurants_Platform.DTOs.FoodItems;
using Restaurants_Platform.Models;

namespace Restaurants_Platform.Mappers;

public static class FoodItemMappers
{
    public static FoodItemDto ToFoodItemDto(this FoodItem foodItem)
    {
        return new FoodItemDto
        {
            Id = foodItem.Id.ToString(),
            Name = foodItem.Name,
            Price = foodItem.Price,
            RestId = foodItem.RestId.ToString()
        };
    }

    public static FoodItem ToFoodItemFromCreate(this CreateFoodItemRequest foodItemDto)
    {
        return new FoodItem
        {
            Name = foodItemDto.Name,
            Price = foodItemDto.Price,
            RestId = foodItemDto.RestId
        };
    }

    public static FoodItem ToFoodItemFromUpdate(this UpdateFoodItemRequestDto foodItemDto)
    {
        return new FoodItem
        {
            Name = foodItemDto.Name,
            Price = foodItemDto.Price
        };
    }
}
