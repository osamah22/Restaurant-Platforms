
using Restaurants_Platform.Models;

namespace Restaurants_Platform.Interfaces
{
    public interface IRestaurantRepository
    {
        public Task<Restaurant?> GetByIdAsync(Guid Id);

        public Task<List<Restaurant>> GetAllAsync();

        public Task<bool> IsExitAsync(Guid Id);

        public Task<Restaurant> AddAsync(Restaurant restaurant);

        public Task<Restaurant?> UpdateAsync(Guid Id, Restaurant restaurant);

        public Task<Restaurant?> DeleteAsync(Guid Id);

    }
}
