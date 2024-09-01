using Microsoft.AspNetCore.Mvc;
using Restaurants_Platform.Interfaces;
using Restaurants_Platform.Models;
using Restaurants_Platform.Dtos.Orders;
using Restaurants_Platform.Mappers;

namespace Restaurants_Platform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;

        public OrderController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<OrderDto>>> GetAll()
        {
            var orders = await _orderRepository.GetAll();
            var orderDtos = orders.Select(o => o.ToOrderDto()).ToList();
            return Ok(orderDtos);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<OrderDto>> GetById(Guid id)
        {
            var order = await _orderRepository.GetById(id);

            if (order == null)
                return NotFound();

            return Ok(order.ToOrderDto());
        }

        [HttpGet("restaurant/{restaurantId:guid}")]
        public async Task<ActionResult<List<OrderDto>>> GetByRestaurantId(Guid restaurantId)
        {
            var orders = await _orderRepository.GetByRestaurantId(restaurantId);

            if (!orders.Any())
                return NotFound();

            var orderDtos = orders.Select(o => o.ToOrderDto()).ToList();
            return Ok(orderDtos);
        }

        [HttpPost]
        public async Task<ActionResult<OrderDto>> Create(CreateOrderRequestDto createOrderDto)
        {
            var order = new Order
            {
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
        public async Task<ActionResult<OrderDto>> Update(Guid id, UpdateOrderRequestDto updateOrderDto)
        {
            var existingOrder = await _orderRepository.GetById(id);

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
        public async Task<ActionResult> Delete(Guid id)
        {
            var order = await _orderRepository.DeleteAsync(id);

            if (order == null)
                return NotFound();

            return NoContent();
        }
    }
}
