using Microsoft.EntityFrameworkCore;
using Restaurants_Platform.Data;
using Restaurants_Platform.Interfaces;
using Restaurants_Platform.Models;
using Restaurants_Platform.Repositories;

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
