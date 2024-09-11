using Restaurants_Platform.DTOs;
using Restaurants_Platform.DTOs.Reviews;
using Restaurants_Platform.Models;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Restaurants_Platform.Mappers;

public static class ReviewsMapper
{
    public static ReviewDto ToReviewDto(this Review review)
    {
        // Ensure that review is not null
        ArgumentNullException.ThrowIfNull(review, nameof(review));

        // Handle the case where AppUser might be null
        var userName = review.AppUser?.FirstName ?? "Unknown User";

        return new ReviewDto
        {
            UserName = userName,
            Content = review.Content ?? "No Content",
            CreatedAt = review.CreatedAt,
            Rate = review.Rate ?? 0 // Assuming a default rating of 0 if Rate is null
        };
    }

    public static Review? ToReviewFromCreate(this CreateReviewDto reviewDto)
    {
        return Review.Create(
            reviewDto.Content,
            (ReviewStar)reviewDto.Star,
            reviewDto.UserId,
            reviewDto.RestaurantId);
    }
}
