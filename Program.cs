using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Restaurants_Platform.Data;
using Restaurants_Platform.Interfaces;
using Restaurants_Platform.Models;
using Restaurants_Platform.Repositories;
using Restaurants_Platform.Services;
using System.Text;

namespace Restaurants_Platform;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddDbContext<ApplicationContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("pg"))
        );

        builder.Services.AddIdentity<AppUser, IdentityRole<Guid>>(options =>
        {
            options.Password.RequiredLength = 8;
            options.Password.RequireUppercase = false;
            options.Password.RequireLowercase = false;
            options.Password.RequiredUniqueChars = 0;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireDigit = false;
            options.Lockout.AllowedForNewUsers = true;
            options.Lockout.MaxFailedAccessAttempts = 10;
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(2);
        })
            .AddEntityFrameworkStores<ApplicationContext>()
            .AddDefaultTokenProviders();

        builder.Services
            .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)),
                    ValidateIssuer = true,
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero // Eliminate any clock skew
                };
            });

        builder.Services.AddScoped<IAccountService, AccountService>();

        builder.Services.AddScoped<ITokenService, TokenService>();

        builder.Services.AddScoped<IRestaurantRepository, RestaurantRepository>();

        builder.Services.AddScoped<IFoodItemRepository, FoodItemRepository>();

        builder.Services.AddScoped<IOrderRepository, OrderRepository>();

        builder.Services.AddControllers();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthentication();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}