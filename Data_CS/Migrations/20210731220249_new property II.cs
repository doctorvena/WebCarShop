using Microsoft.EntityFrameworkCore.Migrations;

namespace Data_CS.Migrations
{
    public partial class newpropertyII : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Identification",
                table: "FinishedItems",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Identification",
                table: "FinishedItems");
        }
    }
}
