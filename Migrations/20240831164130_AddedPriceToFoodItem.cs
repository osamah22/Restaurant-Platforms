using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Restaurants_Platform.Migrations
{
    /// <inheritdoc />
    public partial class AddedPriceToFoodItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "FoodItem",
                type: "numeric(10,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "FoodItem");
        }
    }
}
