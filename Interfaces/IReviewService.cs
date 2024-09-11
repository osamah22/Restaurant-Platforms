using Restaurants_Platform.Models;
using Restaurants_Platform.Shared;

namespace Restaurants_Platform.Interfaces;

public interface IReviewService
{
    public Task<List<Review>> GetAllReviewsAsync();

    public Task<List<Review>> GetRestaurantReviewsAsync(Guid restaurantId);

    public Task<List<Review>> GetByUserIdAsync(Guid userId);

    public Task<Review?> GetByIdAsync(Guid id);

    public Task<Review> CreateAsync(Review review);

    public Task<Result<Review>> UpdateAsync(Guid id, Review review);

    public Task<Review?> DeleteAsync(Guid id);

    public Task<double> GetRestaurantAverageRateAsync(Guid restaurantId);

    public Task<int> GetRestaurantReviewsCountAsync(Guid restaurantId);
}
