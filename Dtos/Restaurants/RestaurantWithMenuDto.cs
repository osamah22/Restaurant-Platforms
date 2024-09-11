
using Restaurants_Platform.DTOs.FoodItems;

namespace Restaurants_Platform.DTOs.Restaurants;

public record RestaurantWithMenuDto(RestaurantDto restaurantDto, List<FoodItemDto> Items);