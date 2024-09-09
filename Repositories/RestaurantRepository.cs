using Microsoft.EntityFrameworkCore;
using Restaurants_Platform.Data;
using Restaurants_Platform.Interfaces;
using Restaurants_Platform.Models;

namespace Restaurants_Platform.Repositories;

public class RestaurantRepository : IRestaurantRepository
{
    private readonly ApplicationContext _context;

    public RestaurantRepository(ApplicationContext context)
    {
        _context = context;
    }

    public async Task<Restaurant?> GetByIdAsync(Guid id)
    {
        return await _context.Restaurants
            .Include(r => r.Menu)
            .FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task<List<Restaurant>> GetAllAsync()
    {
        return await _context.Restaurants.ToListAsync();
    }

    public async Task<bool> IsExitAsync(Guid Id)
    {
        return await _context.Restaurants.AnyAsync(r => r.Id == Id);
    }

    public async Task<Restaurant> AddAsync(Restaurant restaurant)
    {
        restaurant.Id = Guid.NewGuid();
        await _context.Restaurants.AddAsync(restaurant);
        await _context.SaveChangesAsync();
        return restaurant;
    }

    public async Task<Restaurant?> UpdateAsync(Guid Id, Restaurant restaurant)
    {
        var dbRestaurant = await _context.Restaurants.FindAsync(Id);
        if (dbRestaurant is null)
            return null;

        dbRestaurant.Name = restaurant.Name;
        dbRestaurant.Company = restaurant.Company;
        await _context.SaveChangesAsync();
        return dbRestaurant;
    }

    public async Task<Restaurant?> DeleteAsync(Guid Id)
    {
        Restaurant? restaurant = await _context.Restaurants.FindAsync(Id);

        if (restaurant is null)
            return null;

        _context.Remove(restaurant);
        await _context.SaveChangesAsync();

        return restaurant;
    }
}
