using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Restaurants_Platform.Migrations;

/// <inheritdoc />
public partial class RenameOrderItemTable : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_OrderItem_FoodItem_FoodItemId",
            table: "OrderItem");

        migrationBuilder.DropForeignKey(
            name: "FK_OrderItem_Order_OrderId",
            table: "OrderItem");

        migrationBuilder.DropPrimaryKey(
            name: "PK_OrderItem",
            table: "OrderItem");

        migrationBuilder.DropPrimaryKey(
            name: "PK_Order",
            table: "Order");

        migrationBuilder.RenameTable(
            name: "OrderItem",
            newName: "OrderItems");

        migrationBuilder.RenameTable(
            name: "Order",
            newName: "Orders");

        migrationBuilder.RenameIndex(
            name: "IX_OrderItem_FoodItemId",
            table: "OrderItems",
            newName: "IX_OrderItems_FoodItemId");

        migrationBuilder.AddPrimaryKey(
            name: "PK_OrderItems",
            table: "OrderItems",
            columns: new[] { "OrderId", "FoodItemId" });

        migrationBuilder.AddPrimaryKey(
            name: "PK_Orders",
            table: "Orders",
            column: "Id");

        migrationBuilder.AddForeignKey(
            name: "FK_OrderItems_FoodItem_FoodItemId",
            table: "OrderItems",
            column: "FoodItemId",
            principalTable: "FoodItem",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey(
            name: "FK_OrderItems_Orders_OrderId",
            table: "OrderItems",
            column: "OrderId",
            principalTable: "Orders",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_OrderItems_FoodItem_FoodItemId",
            table: "OrderItems");

        migrationBuilder.DropForeignKey(
            name: "FK_OrderItems_Orders_OrderId",
            table: "OrderItems");

        migrationBuilder.DropPrimaryKey(
            name: "PK_Orders",
            table: "Orders");

        migrationBuilder.DropPrimaryKey(
            name: "PK_OrderItems",
            table: "OrderItems");

        migrationBuilder.RenameTable(
            name: "Orders",
            newName: "Order");

        migrationBuilder.RenameTable(
            name: "OrderItems",
            newName: "OrderItem");

        migrationBuilder.RenameIndex(
            name: "IX_OrderItems_FoodItemId",
            table: "OrderItem",
            newName: "IX_OrderItem_FoodItemId");

        migrationBuilder.AddPrimaryKey(
            name: "PK_Order",
            table: "Order",
            column: "Id");

        migrationBuilder.AddPrimaryKey(
            name: "PK_OrderItem",
            table: "OrderItem",
            columns: new[] { "OrderId", "FoodItemId" });

        migrationBuilder.AddForeignKey(
            name: "FK_OrderItem_FoodItem_FoodItemId",
            table: "OrderItem",
            column: "FoodItemId",
            principalTable: "FoodItem",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey(
            name: "FK_OrderItem_Order_OrderId",
            table: "OrderItem",
            column: "OrderId",
            principalTable: "Order",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);
    }
}
