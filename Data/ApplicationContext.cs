
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Restaurants_Platform.Data.Config;
using Restaurants_Platform.Models;

namespace Restaurants_Platform.Data;

public class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions options) : base(options) { }

    // Db Sets
    public DbSet<Restaurant> Restaurants { get; set; }
    public DbSet<FoodItem> FoodItems { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder
            .ApplyConfiguration(new RestaurantConfig())
            .ApplyConfiguration(new FoodItemConfig());

        builder.Entity<Restaurant>()
            .HasMany(restaurant => restaurant.Menu)
            .WithOne(foodItem => foodItem.Restaurant)
            .HasForeignKey(foodItem => foodItem.RestId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Order>()
            .HasMany(o => o.OrderItems)
            .WithOne(i => i.Order)
            .HasForeignKey(i=> i.OrderId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<OrderItem>()
            .HasKey(i => new { i.OrderId, i.FoodItemId });

        base.OnModelCreating(builder);
    }

}
