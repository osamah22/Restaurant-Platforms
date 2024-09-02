using Restaurants_Platform.Dtos.Users;
using Restaurants_Platform.Models;

namespace Restaurants_Platform.Mappers;

public static class AppUserMappers
{
    public static AppUser ToAppUserFromRegisterDto(this RegisterUserDto registerUserDto)
    {
        return new AppUser
        {
            FirstName = registerUserDto.FisrtName,
            LastName = registerUserDto.LastName,
            UserName = registerUserDto.UserName,
            Email = registerUserDto.Email,
        };
    }
}
