using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantManagmentSystem.Core.Migrations
{
    public partial class DBSetSubOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_SubOrder_SubOrderId",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_SubOrder_Orders_OrderId",
                table: "SubOrder");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SubOrder",
                table: "SubOrder");

            migrationBuilder.RenameTable(
                name: "SubOrder",
                newName: "SubOrders");

            migrationBuilder.RenameIndex(
                name: "IX_SubOrder_OrderId",
                table: "SubOrders",
                newName: "IX_SubOrders_OrderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SubOrders",
                table: "SubOrders",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_SubOrders_SubOrderId",
                table: "Categories",
                column: "SubOrderId",
                principalTable: "SubOrders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SubOrders_Orders_OrderId",
                table: "SubOrders",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_SubOrders_SubOrderId",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_SubOrders_Orders_OrderId",
                table: "SubOrders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SubOrders",
                table: "SubOrders");

            migrationBuilder.RenameTable(
                name: "SubOrders",
                newName: "SubOrder");

            migrationBuilder.RenameIndex(
                name: "IX_SubOrders_OrderId",
                table: "SubOrder",
                newName: "IX_SubOrder_OrderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SubOrder",
                table: "SubOrder",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_SubOrder_SubOrderId",
                table: "Categories",
                column: "SubOrderId",
                principalTable: "SubOrder",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SubOrder_Orders_OrderId",
                table: "SubOrder",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
