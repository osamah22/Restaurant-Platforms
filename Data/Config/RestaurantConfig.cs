using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Restaurants_Platform.Models;

namespace Restaurants_Platform.Data.Config
{
    public class RestaurantConfig : IEntityTypeConfiguration<Restaurant>
    {
        public void Configure(EntityTypeBuilder<Restaurant> builder)
        {
            builder.ToTable(nameof(Restaurant))
                .HasKey(r => r.Id)
                .IsClustered(false);

            builder.HasMany(r => r.Menu)
                .WithOne(f => f.Restaurant)
                .HasForeignKey(f => f.RestId);
        }
    }
}
