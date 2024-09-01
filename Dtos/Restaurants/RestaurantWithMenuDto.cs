
using Restaurants_Platform.Dtos.FoodItems;

namespace Restaurants_Platform.Dtos.Restaurants;

public record RestaurantWithMenuDto(RestaurantDto restaurantDto, List<FoodItemDto> Items);