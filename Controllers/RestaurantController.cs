using Microsoft.AspNetCore.Mvc;
using Restaurants_Platform.Dtos.Restaurants;
using Restaurants_Platform.Interfaces;
using Restaurants_Platform.Mappers;
using Restaurants_Platform.Models;

namespace Restaurants_Platform.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RestaurantController : ControllerBase
{
    private readonly IRestaurantRepository _restaurantRepo;

    public RestaurantController(IRestaurantRepository restaurantRepo)
    {
        _restaurantRepo = restaurantRepo;
    }


    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var restaurants = await _restaurantRepo.GetAllAsync();
        var restaurantsDto = restaurants.Select(r => r.ToRestaurantDto());

        return Ok(restaurantsDto);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _restaurantRepo.GetByIdAsync(id);

        if (result is null)
            return NotFound();

        return Ok(result.ToRestaurantWithMenuDto());
    }

    [HttpPost]
    public async Task<IActionResult> Add(CreateRestaurantRequestDto restaurantDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        Restaurant restaurant = await _restaurantRepo.AddAsync(restaurantDto.ToRestaurantFromCreate());

        return CreatedAtAction(nameof(GetById), new { id = restaurant.Id }, restaurant.ToRestaurantDto());
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, UpdateRestaurantRequestDto restaurantDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var restaurant = await _restaurantRepo.UpdateAsync(id, restaurantDto.ToRestaurantFromUpdate());

        if (restaurant is null)
            return NotFound();

        return Ok(restaurant.ToRestaurantDto());
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var restaurant = await _restaurantRepo.DeleteAsync(id);

        if (restaurant is null)
            return NotFound();

        return NoContent();
    }

}
