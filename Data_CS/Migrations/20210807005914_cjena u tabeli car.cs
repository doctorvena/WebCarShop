using Microsoft.EntityFrameworkCore.Migrations;

namespace Data_CS.Migrations
{
    public partial class cjenautabelicar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "Price",
                table: "Car",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "Car");
        }
    }
}
