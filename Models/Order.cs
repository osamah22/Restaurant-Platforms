using Restaurants_Platform.Data;

namespace Restaurants_Platform.Models;

public class Order
{
    public Guid Id { get; set; }

    public DateTime CreatedAt { get; set; }

    public Guid AppUserId { get; set; }

    public int StatusId { get; set; } = OrderStatusMap.Preparing.Id;

    public AppUser? AppUser { get; set; }

    public OrderStatus? Status { get; set; }

    public List<OrderItem>? OrderItems { get; set; }
}
