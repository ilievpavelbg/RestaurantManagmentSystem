using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantManagmentSystem.Core.Migrations
{
    public partial class FKSuborderIdSetToNullasbe : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_SubOrders_SubOrderId",
                table: "Categories");

            migrationBuilder.AlterColumn<int>(
                name: "SubOrderId",
                table: "Categories",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_SubOrders_SubOrderId",
                table: "Categories",
                column: "SubOrderId",
                principalTable: "SubOrders",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_SubOrders_SubOrderId",
                table: "Categories");

            migrationBuilder.AlterColumn<int>(
                name: "SubOrderId",
                table: "Categories",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_SubOrders_SubOrderId",
                table: "Categories",
                column: "SubOrderId",
                principalTable: "SubOrders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
