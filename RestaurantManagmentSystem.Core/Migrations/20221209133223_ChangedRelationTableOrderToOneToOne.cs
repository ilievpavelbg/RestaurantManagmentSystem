using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantManagmentSystem.Core.Migrations
{
    public partial class ChangedRelationTableOrderToOneToOne : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "Tables",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tables_OrderId",
                table: "Tables",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tables_Orders_OrderId",
                table: "Tables",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tables_Orders_OrderId",
                table: "Tables");

            migrationBuilder.DropIndex(
                name: "IX_Tables_OrderId",
                table: "Tables");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "Tables");
        }
    }
}
