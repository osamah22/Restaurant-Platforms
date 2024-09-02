namespace Restaurants_Platform.Models;

public class Order
{
    public Guid Id { get; set; }

    public DateTime CreatedAt { get; set; }

    public Guid AppUserId { get; set; }

    public AppUser? AppUser { get; set; }

    public virtual List<OrderItem>? OrderItems { get; set; }
}
