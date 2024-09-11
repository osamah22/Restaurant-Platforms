using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurants_Platform.DTOs.Orders;
using Restaurants_Platform.Interfaces;
using Restaurants_Platform.Mappers;
using Restaurants_Platform.Models;

namespace Restaurants_Platform.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class OrderController : ControllerBase
{
    private readonly IOrderRepository _orderRepository;

    public OrderController(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    [HttpGet("All")]
    public async Task<IActionResult> GetAll()
    {
        var orders = await _orderRepository.GetAllAsync();
        var orderDtos = orders.Select(o => o.ToOrderDto()).ToList();
        return Ok(orderDtos);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var order = await _orderRepository.GetByIdAsync(id);

        if (order == null)
            return NotFound();

        return Ok(order.ToOrderDto());
    }

    [HttpGet($"{nameof(Restaurant)}/{{restaurantId:guid}}")]
    public async Task<IActionResult> GetByRestaurantId(Guid restaurantId)
    {
        var orders = await _orderRepository.GetByRestaurantIdAsync(restaurantId);

        if (!orders.Any())
            return NotFound();

        var orderDtos = orders.Select(o => o.ToOrderDto()).ToList();
        return Ok(orderDtos);
    }

    [HttpGet]
    public async Task<IActionResult> GetUserOrders()
    {
        var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        //Guid userId = Guid.Empty;
        if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
        {
            return Unauthorized("Invalid or missing user ID.");
        }

        var userOrders = await _orderRepository.GetByUserIdAsync(userId);

        return Ok(userOrders.Select(o => o.ToOrderDto()));
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateOrderRequestDto createOrderDto)
    {
        var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
        {
            return Unauthorized("Invalid or missing user ID.");
        }

        var order = new Order
        {
            AppUserId = userId,
            OrderItems = createOrderDto.Items.Select(i => new OrderItem
            {
                FoodItemId = i.FoodItemId,
                Quantity = i.Quantity
            }).ToList()
        };

        var createdOrder = await _orderRepository.CreateAsync(order);

        return CreatedAtAction(nameof(GetById), new { id = createdOrder.Id }, createdOrder.ToOrderDto());
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, UpdateOrderRequestDto updateOrderDto)
    {
        var existingOrder = await _orderRepository.GetByIdAsync(id);

        if (existingOrder == null)
            return NotFound();

        existingOrder.OrderItems = updateOrderDto.Items.Select(i => new OrderItem
        {
            FoodItemId = i.FoodItemId,
            Quantity = i.Quantity
        }).ToList();

        var updatedOrder = await _orderRepository.UpdateAsync(id, existingOrder);

        if (updatedOrder == null)
            return NotFound();

        return Ok(updatedOrder.ToOrderDto());
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var order = await _orderRepository.DeleteAsync(id);

        if (order == null)
            return NotFound();

        return NoContent();
    }
}
