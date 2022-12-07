using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantManagmentSystem.Core.Migrations
{
    public partial class AddOnStockAndOrderQtyToMenuItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OnStock",
                table: "MenuItems",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrderedQty",
                table: "MenuItems",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OnStock",
                table: "MenuItems");

            migrationBuilder.DropColumn(
                name: "OrderedQty",
                table: "MenuItems");
        }
    }
}
