using Microsoft.EntityFrameworkCore;
using Restaurants_Platform.Data;
using Restaurants_Platform.Interfaces;
using Restaurants_Platform.Models;

namespace Restaurants_Platform.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly ApplicationContext _context;

    public OrderRepository(ApplicationContext context)
    {
        _context = context;
    }

    public async Task<List<Order>> GetAllAsync()
    {
        return await _context.Orders
            .Include(o => o.OrderItems!)
            .ThenInclude(oi => oi.FoodItem)
            .ThenInclude(fi => fi!.Restaurant)  // Optional: If you need Restaurant details
            .ToListAsync();
    }

    public async Task<List<Order>> GetByUserIdAsync(Guid appUserId)
    {
        return await _context.Orders
          .Include(o => o.OrderItems!)
          .ThenInclude(oi => oi.FoodItem)
          .ThenInclude(fi => fi!.Restaurant)
          .Where(o => o.AppUserId == appUserId)
          .ToListAsync();
    }

    public async Task<List<Order>> GetByRestaurantIdAsync(Guid RestId)
    {
        return await _context.Orders
          .Include(o => o.OrderItems!)
          .ThenInclude(oi => oi.FoodItem)
          .ThenInclude(fi => fi!.Restaurant)
          .Where(o => o.OrderItems!.Any(i => i.FoodItem!.RestId == RestId))
          .ToListAsync();
    }

    public async Task<Order?> GetByIdAsync(Guid Id)
    {
        return await _context.Orders
            .Include(o => o.OrderItems!)
            .ThenInclude(oi => oi.FoodItem)
            .ThenInclude(fi => fi!.Restaurant) 
            .FirstOrDefaultAsync(o => o.Id == Id);
    }

    public async Task<Order> CreateAsync(Order order)
    {
        order.Id = Guid.NewGuid();
        order.CreatedAt = DateTime.UtcNow;

        await _context.Orders.AddAsync(order);
        await _context.SaveChangesAsync();
        var newOrder = await GetByIdAsync(order.Id) ?? order; // Joining the tables
        return newOrder; 
    }

    public async Task<Order?> UpdateAsync(Guid Id, Order order)
    {
        var existingOrder = await _context.Orders
            .Include(o => o.OrderItems)
            .FirstOrDefaultAsync(o => o.Id == Id);

        if (existingOrder is null)
            return null;

        // Update basic properties
        _context.Entry(existingOrder).CurrentValues.SetValues(order);

        // Handle OrderItems update
        foreach (var orderItem in order.OrderItems!)
        {
            var existingItem = existingOrder.OrderItems!
                .FirstOrDefault(oi => oi.FoodItemId == orderItem.FoodItemId);

            if (existingItem != null)
            {
                // Update existing item
                _context.Entry(existingItem).CurrentValues.SetValues(orderItem);
            }
            else
            {
                // Add new item
                existingOrder.OrderItems!.Add(orderItem);
            }
        }

        // Remove items that are no longer in the updated list
        foreach (var existingItem in existingOrder.OrderItems!.ToList())
        {
            if (!order.OrderItems.Any(oi => oi.FoodItemId == existingItem.FoodItemId))
            {
                existingOrder.OrderItems!.Remove(existingItem);
            }
        }

        await _context.SaveChangesAsync();
        existingOrder = await GetByIdAsync(order.Id) ?? existingOrder; // Joining the tables
        return existingOrder;
    }

    public async Task<Order?> DeleteAsync(Guid Id)
    {
        var order = await _context.Orders.FindAsync(Id);
        if (order is null)
            return null;

        _context.Orders.Remove(order);
        await _context.SaveChangesAsync();
        return order;
    }
}
