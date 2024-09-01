using Restaurants_Platform.Models;

namespace Restaurants_Platform.Interfaces
{
    public interface IFoodItemRepository
    {
        public Task<List<FoodItem>> GetAllAsync();

        public Task<FoodItem?> GetByIdAsync(Guid id);

        public Task<List<FoodItem>> GetByRestaurantIdAsync(Guid RestId);

        public Task<FoodItem> CreateAsync(FoodItem item);

        public Task<FoodItem?> UpdateAsync(Guid Id, FoodItem item);

        public Task<FoodItem?> DeleteAsync(Guid Id);
    }
}
