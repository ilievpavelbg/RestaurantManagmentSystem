using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantManagmentSystem.Core.Migrations
{
    public partial class AdTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TempOrderMenuItemViewModels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OnStock = table.Column<int>(type: "int", nullable: true),
                    OrderedQty = table.Column<int>(type: "int", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ImageURL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ItemsForCooking = table.Column<bool>(type: "bit", nullable: true),
                    IsChecked = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TempOrderMenuItemViewModels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TempOrders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemId = table.Column<int>(type: "int", nullable: true),
                    ItemName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ItemsForCooking = table.Column<bool>(type: "bit", nullable: true),
                    IsChecked = table.Column<bool>(type: "bit", nullable: true),
                    OrderId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TempOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TempOrders_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TempOrderTempOrderMenuItemViewModel",
                columns: table => new
                {
                    MenuItemsId = table.Column<int>(type: "int", nullable: false),
                    TempOrdersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TempOrderTempOrderMenuItemViewModel", x => new { x.MenuItemsId, x.TempOrdersId });
                    table.ForeignKey(
                        name: "FK_TempOrderTempOrderMenuItemViewModel_TempOrderMenuItemViewModels_MenuItemsId",
                        column: x => x.MenuItemsId,
                        principalTable: "TempOrderMenuItemViewModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TempOrderTempOrderMenuItemViewModel_TempOrders_TempOrdersId",
                        column: x => x.TempOrdersId,
                        principalTable: "TempOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TempOrders_OrderId",
                table: "TempOrders",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_TempOrderTempOrderMenuItemViewModel_TempOrdersId",
                table: "TempOrderTempOrderMenuItemViewModel",
                column: "TempOrdersId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TempOrderTempOrderMenuItemViewModel");

            migrationBuilder.DropTable(
                name: "TempOrderMenuItemViewModels");

            migrationBuilder.DropTable(
                name: "TempOrders");
        }
    }
}
