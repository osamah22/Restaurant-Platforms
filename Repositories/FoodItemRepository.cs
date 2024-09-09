using Microsoft.EntityFrameworkCore;
using Restaurants_Platform.Data;
using Restaurants_Platform.Interfaces;
using Restaurants_Platform.Models;

namespace Restaurants_Platform.Repositories;

public class FoodItemRepository : IFoodItemRepository
{
    private readonly ApplicationContext _context;
    public FoodItemRepository(ApplicationContext context)
    {
        _context = context;
    }

    public async Task<FoodItem?> GetByIdAsync(Guid id)
    {
        return await _context.FoodItems.FindAsync(id);
    }

    public async Task<List<FoodItem>> GetAllAsync()
    {
        return await _context.FoodItems.ToListAsync();
    }

    public async Task<List<FoodItem>> GetByRestaurantIdAsync(Guid RestId)
    {
        return await _context.FoodItems.Where(item => item.RestId == RestId).ToListAsync();
    }

    public async Task<FoodItem> CreateAsync(FoodItem item)
    {
        item.Id = Guid.NewGuid();
        await _context.FoodItems.AddAsync(item);
        await _context.SaveChangesAsync();
        return item;
    }

    public async Task<FoodItem?> UpdateAsync(Guid Id, FoodItem item)
    {
        var dbItem = await _context.FoodItems.FindAsync(Id);

        if (dbItem is null)
            return null;

        dbItem.Name = item.Name;
        dbItem.Price = item.Price;
        await _context.SaveChangesAsync();
        return dbItem;
    }

    public async Task<FoodItem?> DeleteAsync(Guid Id)
    {
        var item = await _context.FoodItems.FindAsync(Id);

        if (item is null)
            return item;

        _context.FoodItems.Remove(item);
        await _context.SaveChangesAsync();

        return item;
    }

}
