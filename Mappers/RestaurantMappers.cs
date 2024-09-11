using Restaurants_Platform.DTOs.Restaurants;
using Restaurants_Platform.Models;

namespace Restaurants_Platform.Mappers;

public static class RestaurantMappers
{
    public static RestaurantDto ToRestaurantDto(this Restaurant restaurantModel)
    {
        return new RestaurantDto(restaurantModel.Id.ToString(), restaurantModel.Name, restaurantModel.Company);
    }

    public static RestaurantWithMenuDto ToRestaurantWithMenuDto(this Restaurant restaurant)
    {
        return new RestaurantWithMenuDto(restaurant.ToRestaurantDto(),
            restaurant.Menu!.Select(i => i.ToFoodItemDto()).ToList());
    }

    public static Restaurant ToRestaurantFromCreate(this CreateRestaurantRequestDto restaurantDto)
    {
        return new Restaurant
        {
            Name = restaurantDto.Name,
            Company = restaurantDto.Company,
        };
    }

    public static Restaurant ToRestaurantFromUpdate(this UpdateRestaurantRequestDto restaurantDto)
    {
        return new Restaurant()
        {
            Name = restaurantDto.Name,
            Company = restaurantDto.Company,
        };
    }
}
