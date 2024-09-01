namespace Restaurants_Platform.Dtos.FoodItems
{
    public class FoodItemDto
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string RestId { get; set; } = string.Empty;
    }
}
