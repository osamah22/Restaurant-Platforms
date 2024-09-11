using Restaurants_Platform.Shared;
using Restaurants_Platform.Errors;

namespace Restaurants_Platform.Models;

public class Review
{
    public Guid Id { get; private set; }

    public string? Content { get; private set; }

    public int? Rate { get; private set; } //[1 to 5 stars]

    public DateTime CreatedAt { get; private set; }

    // Foreign relations
    public Guid UserId { get; private set; }

    public Guid RestaurantId { get; private set; }

    //Nav properties
    public AppUser? AppUser { get; set; }

    public Restaurant? Restaurant { get; set; }

    private Review() { } // private constructor (Do not change this) you can modify the Create method below if needed

    /// <summary>
    /// This method provides a way to update the content and the rate of a review.
    /// It throws an error if both content AND star are null.
    /// </summary>
    /// <param name="content">The new content for the review. Can be null or empty.</param>
    /// <param name="star">The new star rating for the review. Must be between 1 and 5.</param>
    /// <exception cref="BothContentAndStarCannotBeNullException">Thrown when both content and star rating are null or empty.</exception>
    public Result Update(string? content, ReviewStar? star)
    {
        if (star is null && string.IsNullOrEmpty(content)) // You have to at least have one of these two to update the review
            return Result.Failure(EntitiesErrors.Review.ContentAndRateIsNull);

        Content = content;
        Rate = star is null ? null : (int)star;
        return Result.Success();
    }

    /// <summary>
    /// This method provides a way to create Review Or null if state is not valid
    /// </summary>
    /// <param name="content"></param>
    /// <param name="star"></param>
    /// <param name="userId"></param>
    /// <param name="restaurantId"></param>
    /// <returns>Review Or null if it's not valid</returns>
    public static Review? Create(string? content, ReviewStar? star, Guid userId, Guid restaurantId)
    {
        if (star is null && string.IsNullOrEmpty(content)) // You have to at least have one of these two to create a review
            return null;

        return new()
        {
            Id = Guid.NewGuid(),
            Content = content,
            Rate = star is null ? null : (int)star,
            CreatedAt = DateTime.UtcNow,
            UserId = userId, 
            RestaurantId = restaurantId
        };
    }
}

// Available Rating Stars
public enum ReviewStar
{
    OneStar = 1,
    TwoStars = 2,
    ThreeStars = 3,
    FourStars = 4,
    FiveStars = 5
}