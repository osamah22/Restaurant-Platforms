using Restaurants_Platform.Models;

namespace Restaurants_Platform.Interfaces;

public interface IOrderRepository
{
    public Task<List<Order>> GetAllAsync();

    public Task<List<Order>> GetByUserIdAsync(Guid appUserId);

    public Task<List<Order>> GetByRestaurantIdAsync(Guid Id);

    public Task<Order?> GetByIdAsync(Guid Id);

    public Task<Order> CreateAsync(Order order);

    public Task<Order?> UpdateAsync(Guid Id, Order order);

    public Task<Order?> DeleteAsync(Guid Id);
}
