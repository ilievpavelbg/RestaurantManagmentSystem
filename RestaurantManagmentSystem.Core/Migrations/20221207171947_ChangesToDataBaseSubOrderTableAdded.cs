using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantManagmentSystem.Core.Migrations
{
    public partial class ChangesToDataBaseSubOrderTableAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MenuItems_Categories_CategoryId",
                table: "MenuItems");

            migrationBuilder.DropTable(
                name: "MenuItemOrder");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "Orders",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "MenuItems",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SubOrderId",
                table: "Categories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "SubOrder",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsCompleted = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreateOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CompletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CurrentTotalSum = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubOrder", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubOrder_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_SubOrderId",
                table: "Categories",
                column: "SubOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_SubOrder_OrderId",
                table: "SubOrder",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_SubOrder_SubOrderId",
                table: "Categories",
                column: "SubOrderId",
                principalTable: "SubOrder",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MenuItems_Categories_CategoryId",
                table: "MenuItems",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_SubOrder_SubOrderId",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_MenuItems_Categories_CategoryId",
                table: "MenuItems");

            migrationBuilder.DropTable(
                name: "SubOrder");

            migrationBuilder.DropIndex(
                name: "IX_Categories_SubOrderId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "SubOrderId",
                table: "Categories");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "Orders",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "MenuItems",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "MenuItemOrder",
                columns: table => new
                {
                    MenuItemsId = table.Column<int>(type: "int", nullable: false),
                    OrdersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuItemOrder", x => new { x.MenuItemsId, x.OrdersId });
                    table.ForeignKey(
                        name: "FK_MenuItemOrder_MenuItems_MenuItemsId",
                        column: x => x.MenuItemsId,
                        principalTable: "MenuItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MenuItemOrder_Orders_OrdersId",
                        column: x => x.OrdersId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MenuItemOrder_OrdersId",
                table: "MenuItemOrder",
                column: "OrdersId");

            migrationBuilder.AddForeignKey(
                name: "FK_MenuItems_Categories_CategoryId",
                table: "MenuItems",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id");
        }
    }
}
