using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurants_Platform.Data;
using Restaurants_Platform.Interfaces;
using Restaurants_Platform.Mappers;
using Restaurants_Platform.Models;

namespace Restaurants_Platform.Controllers;

[Route($"api/{nameof(Order)}/Status")]
[ApiController]
public class OrderStatusController : ControllerBase
{
    private readonly IOrderRepository _orderRepository;

    public OrderStatusController(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    [HttpGet("/api/Status")]
    public IActionResult GetAll()
    {
        return Ok(OrderStatusMap.GetAllOrderStatuses().ToList());
    }

    [HttpGet("/api/{orderId:guid}/Status")]
    public async Task<IActionResult> GetStatus(Guid orderId)
    {
        var order = await _orderRepository.GetByIdAsync(orderId);

        if (order is null)
            return NotFound();

        var status = OrderStatusMap.GetAllOrderStatuses().FirstOrDefault(s => s.Id == order.StatusId);

        return Ok(status);
    }


    [HttpPatch("/api/{orderId:guid}/Status/{statusId:int}")]
    public async Task<IActionResult> ChangeStatus(Guid orderId, int statusId)
    {
        var status = OrderStatusMap.GetAllOrderStatuses().FirstOrDefault(s => s.Id == statusId);
        if (status is null)
            return BadRequest("Status does not exit");

        var order = await _orderRepository.GetByIdAsync(orderId);

        if (order is null)
            return NotFound("Order does not exit");

        order.StatusId = status.Id;
        order = await _orderRepository.UpdateAsync(order.Id, order);

        return Ok(order!.ToOrderDto());
    }
}
