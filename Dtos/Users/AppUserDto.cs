﻿namespace Restaurants_Platform.Dtos.Users;

public class AppUserDto
{
    public Guid Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}
