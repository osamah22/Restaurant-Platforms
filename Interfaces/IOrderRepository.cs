using Restaurants_Platform.Models;

namespace Restaurants_Platform.Interfaces;

public interface IOrderRepository
{
    public Task<List<Order>> GetAll();

    public Task<List<Order>> GetByRestaurantId(Guid Id);

    public Task<Order?> GetById(Guid Id);

    public Task<Order> CreateAsync(Order order);

    public Task<Order?> UpdateAsync(Guid Id, Order order);

    public Task<Order?> DeleteAsync(Guid Id);
}
