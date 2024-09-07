using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Restaurants_Platform.Models;

namespace Restaurants_Platform.Data;

public class ApplicationContext : IdentityDbContext<AppUser, IdentityRole<Guid>, Guid>
{
    public ApplicationContext(DbContextOptions options) : base(options) { }

    // Db Sets
    public DbSet<Restaurant> Restaurants { get; set; }
    public DbSet<FoodItem> FoodItems { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<OrderStatus> OrderStatuses { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {

        //Making PKs Not Clustered
        builder.Entity<AppUser>()
            .HasKey(user => user.Id)
            .IsClustered(false);

        builder.Entity<Restaurant>()
            .HasKey(r => r.Id)
            .IsClustered(false);

        builder.Entity<FoodItem>()
            .HasKey(item => item.Id)
            .IsClustered(false);

        builder.Entity<Order>()
            .HasKey(o => o.Id)
            .IsClustered(false);

        // OrderItem composite key 
        builder.Entity<OrderItem>()
            .HasKey(item => new { item.OrderId, item.FoodItemId });

        //Making Relationships between entities
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

        builder.Entity<AppUser>()
            .HasMany(u => u.Orders)
            .WithOne(o => o.AppUser)
            .HasForeignKey(o => o.AppUserId)
            .OnDelete(DeleteBehavior.Cascade);

        // Order Status Seeding
        builder.Entity<OrderStatus>()
            .HasData(OrderStatusMap.GetAllOrderStatuses());

        base.OnModelCreating(builder);
    }

}
