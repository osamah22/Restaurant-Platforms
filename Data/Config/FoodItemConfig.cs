using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Restaurants_Platform.Models;

namespace Restaurants_Platform.Data.Config
{
    public class FoodItemConfig : IEntityTypeConfiguration<FoodItem>
    {
        public void Configure(EntityTypeBuilder<FoodItem> builder)
        {
            builder.ToTable(nameof(FoodItem))
                .HasKey(i => i.Id)
                .IsClustered(false);

            builder.Property(i => i.Price)
                .HasColumnType("decimal(10,2)");
        }
    }
}
