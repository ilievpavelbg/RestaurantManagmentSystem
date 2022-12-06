using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantManagmentSystem.Core.Migrations
{
    public partial class UserIdAddedToTableEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Tables",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Tables");
        }
    }
}
