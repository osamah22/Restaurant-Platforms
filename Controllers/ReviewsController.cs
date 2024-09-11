using Microsoft.AspNetCore.Mvc;
using Restaurants_Platform.DTOs;
using Restaurants_Platform.Interfaces;
using Restaurants_Platform.Mappers;
using Restaurants_Platform.Models;

namespace Restaurants_Platform.Controllers;

[Route("api/reviews")]
[ApiController]
public class ReviewsController : ControllerBase
{
    private readonly IReviewService _reviewService;

    public ReviewsController(IReviewService reviewService)
    {
        _reviewService = reviewService;
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAll()
    {
        var reviews = await _reviewService.GetAllReviewsAsync();
        var reviewsDtos = reviews.Select(r => r.ToReviewDto());
        return Ok(reviewsDtos);
    }
    
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetAll(Guid id)
    {
        var reviews = await _reviewService.GetAllReviewsAsync();
        var reviewsDtos = reviews.Select(r => r.ToReviewDto());
        return Ok(reviewsDtos);
    }

    [HttpGet("/api/users/{username}/reviews")]
    public async Task<IActionResult> GetUsersReviews()
    {
        /*To-Do*/
        await Task.Delay(0);
        return StatusCode(501, new { message = "This method is not implemented yet." });
    }
}
