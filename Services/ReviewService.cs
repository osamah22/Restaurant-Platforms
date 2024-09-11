using Microsoft.EntityFrameworkCore;
using Restaurants_Platform.Data;
using Restaurants_Platform.Errors;
using Restaurants_Platform.Interfaces;
using Restaurants_Platform.Models;
using Restaurants_Platform.Shared;

namespace Restaurants_Platform.Services;

// The reason this is a service not a repository is that it has some other methods that are not related to repository like Counting The average rating,
// therefore I decided it'a a service
public class ReviewService : IReviewService
{
    private readonly ApplicationContext _context;

    public ReviewService(ApplicationContext context)
    {
        _context = context;
    }

    public async Task<List<Review>> GetAllReviewsAsync()
    {
        return await _context.Reviews
            .Include(r => r.AppUser)
            .Include(r => r.Restaurant)
            .ToListAsync();
    }

    public async Task<List<Review>> GetRestaurantReviewsAsync(Guid restaurantId)
    {
        return await _context.Reviews
            .Where(r => r.RestaurantId == restaurantId)
            .ToListAsync();
    }

    public async Task<List<Review>> GetByUserIdAsync(Guid userId)
    {
        return await _context.Reviews
            .Include(r => r.AppUser)
            .Include(r => r.Restaurant)
            .Where(r => r.UserId == userId)
            .ToListAsync();
    }

    public async Task<Review?> GetByIdAsync(Guid id)
    {
        return await _context.Reviews
            .Include(r => r.AppUser)
            .Include(r => r.Restaurant)
            .FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task<Review> CreateAsync(Review review)
    {
        await _context.Reviews.AddAsync(review);
        await _context.SaveChangesAsync();
        return review;
    }

    public async Task<Result<Review>> UpdateAsync(Guid id, Review review)
    {
        var existingReview = await _context.Reviews.FindAsync(id);
        if (existingReview is null)
            return Result.Failure<Review>(EntitiesErrors.Review.NotFound(id));

        // Convert int? to ReviewStar?
        ReviewStar? star = review.Rate.HasValue ? (ReviewStar)review.Rate.Value : null;
        var reviewResult = existingReview.Update(review.Content, star);

        if(reviewResult.IsSuccess)
        {
            await _context.SaveChangesAsync();
            return Result.Success<Review>(existingReview);
        }

        return Result.Failure<Review>(reviewResult.Error);
    }

    public async Task<Review?> DeleteAsync(Guid id)
    {
        var review = await _context.Reviews.FindAsync(id);

        if (review is null)
            return null;

        _context.Reviews.Remove(review);
        await _context.SaveChangesAsync();

        return review;
    }

    public async Task<double> GetRestaurantAverageRateAsync(Guid restaurantId)
    {
        var averageRate = await _context.Reviews
            .Where(review => review.RestaurantId == restaurantId && review.Rate.HasValue)
            .AverageAsync(review => (double)review.Rate!.Value);

        return averageRate;
    }

    public async Task<int> GetRestaurantReviewsCountAsync(Guid restaurantId)
    {
        return await _context.Reviews.CountAsync(review => review.RestaurantId == restaurantId);
    }
}
