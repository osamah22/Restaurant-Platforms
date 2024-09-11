using Microsoft.AspNetCore.Mvc;
using Restaurants_Platform.DTOs.FoodItems;
using Restaurants_Platform.Interfaces;
using Restaurants_Platform.Mappers;
using Restaurants_Platform.Models;

namespace Restaurants_Platform.Controllers;

[ApiController]
[Route($"api/{nameof(Restaurant)}/{nameof(FoodItem)}")]
public class FoodItemController : ControllerBase
{
    private readonly IFoodItemRepository _foodItemRepo;
    private readonly IRestaurantRepository _restaurantRepo;

    public FoodItemController(IFoodItemRepository foodItemRepo, IRestaurantRepository restaurantRepo)
    {
        _foodItemRepo = foodItemRepo;
        _restaurantRepo = restaurantRepo;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var items = await _foodItemRepo.GetAllAsync();
        var itemsDto = items.Select(i => i.ToFoodItemDto());

        return Ok(itemsDto);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var item = await _foodItemRepo.GetByIdAsync(id);

        if (item is null)
            return NotFound();

        return Ok(item.ToFoodItemDto());
    }

    [HttpPost]
    public async Task<IActionResult> AddItem(CreateFoodItemRequest itemDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (!await _restaurantRepo.IsExitAsync(itemDto.RestId))
            return NotFound("Restaurant does not exit");

        var item = await _foodItemRepo.CreateAsync(itemDto.ToFoodItemFromCreate());

        return CreatedAtAction(nameof(GetById), new { id = item.Id }, item.ToFoodItemDto());
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, UpdateFoodItemRequestDto updateDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var item = await _foodItemRepo.UpdateAsync(id, updateDto.ToFoodItemFromUpdate());

        if (item is null)
            return NotFound();

        return Ok(item.ToFoodItemDto());
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var item = await _foodItemRepo.DeleteAsync(id);

        if (item is null)
            return NotFound();

        return NoContent();
    }
}
