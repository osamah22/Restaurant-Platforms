using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Restaurants_Platform.Migrations;

/// <inheritdoc />
public partial class RenamedTables : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_FoodItem_Restaurant_RestId",
            table: "FoodItem");

        migrationBuilder.DropForeignKey(
            name: "FK_OrderItems_FoodItem_FoodItemId",
            table: "OrderItems");

        migrationBuilder.DropPrimaryKey(
            name: "PK_Restaurant",
            table: "Restaurant");

        migrationBuilder.DropPrimaryKey(
            name: "PK_FoodItem",
            table: "FoodItem");

        migrationBuilder.RenameTable(
            name: "Restaurant",
            newName: "Restaurants");

        migrationBuilder.RenameTable(
            name: "FoodItem",
            newName: "FoodItems");

        migrationBuilder.RenameIndex(
            name: "IX_FoodItem_RestId",
            table: "FoodItems",
            newName: "IX_FoodItems_RestId");

        migrationBuilder.AlterColumn<decimal>(
            name: "Price",
            table: "FoodItems",
            type: "numeric",
            nullable: false,
            oldClrType: typeof(decimal),
            oldType: "numeric(10,2)");

        migrationBuilder.AddPrimaryKey(
            name: "PK_Restaurants",
            table: "Restaurants",
            column: "Id");

        migrationBuilder.AddPrimaryKey(
            name: "PK_FoodItems",
            table: "FoodItems",
            column: "Id");

        migrationBuilder.AddForeignKey(
            name: "FK_FoodItems_Restaurants_RestId",
            table: "FoodItems",
            column: "RestId",
            principalTable: "Restaurants",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey(
            name: "FK_OrderItems_FoodItems_FoodItemId",
            table: "OrderItems",
            column: "FoodItemId",
            principalTable: "FoodItems",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_FoodItems_Restaurants_RestId",
            table: "FoodItems");

        migrationBuilder.DropForeignKey(
            name: "FK_OrderItems_FoodItems_FoodItemId",
            table: "OrderItems");

        migrationBuilder.DropPrimaryKey(
            name: "PK_Restaurants",
            table: "Restaurants");

        migrationBuilder.DropPrimaryKey(
            name: "PK_FoodItems",
            table: "FoodItems");

        migrationBuilder.RenameTable(
            name: "Restaurants",
            newName: "Restaurant");

        migrationBuilder.RenameTable(
            name: "FoodItems",
            newName: "FoodItem");

        migrationBuilder.RenameIndex(
            name: "IX_FoodItems_RestId",
            table: "FoodItem",
            newName: "IX_FoodItem_RestId");

        migrationBuilder.AlterColumn<decimal>(
            name: "Price",
            table: "FoodItem",
            type: "numeric(10,2)",
            nullable: false,
            oldClrType: typeof(decimal),
            oldType: "numeric");

        migrationBuilder.AddPrimaryKey(
            name: "PK_Restaurant",
            table: "Restaurant",
            column: "Id");

        migrationBuilder.AddPrimaryKey(
            name: "PK_FoodItem",
            table: "FoodItem",
            column: "Id");

        migrationBuilder.AddForeignKey(
            name: "FK_FoodItem_Restaurant_RestId",
            table: "FoodItem",
            column: "RestId",
            principalTable: "Restaurant",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey(
            name: "FK_OrderItems_FoodItem_FoodItemId",
            table: "OrderItems",
            column: "FoodItemId",
            principalTable: "FoodItem",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);
    }
}
