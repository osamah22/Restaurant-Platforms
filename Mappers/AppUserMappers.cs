using Restaurants_Platform.DTOs.Users;
using Restaurants_Platform.Models;

namespace Restaurants_Platform.Mappers;

public static class AppUserMappers
{
    public static AppUser ToAppUserFromRegisterDto(this RegisterUserDto registerUserDto)
    {
        return new AppUser
        {
            FirstName = registerUserDto.FirstName,
            LastName = registerUserDto.LastName,
            UserName = registerUserDto.UserName,
            Email = registerUserDto.Email,
        };
    }
    public static AppUserDto ToAppUserDto(this AppUser appUser)
    {
        // To Supress null warnings
        ArgumentNullException.ThrowIfNull(appUser.Email);
        ArgumentNullException.ThrowIfNull(appUser.UserName);

        return new()
        {
            Id = appUser.Id,
            Username = appUser.UserName.ToLower(),
            Email = appUser.Email.ToLower(),
        };
    }
}