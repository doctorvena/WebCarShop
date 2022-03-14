using Microsoft.EntityFrameworkCore.Migrations;

namespace Data_CS.Migrations
{
    public partial class UpdateTableCarAndCarModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CarModelName",
                table: "CarModel");

            migrationBuilder.DropColumn(
                name: "Brand",
                table: "Car");

            migrationBuilder.AddColumn<int>(
                name: "BrandID",
                table: "CarModel",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "NazivModela",
                table: "CarModel",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BrandID",
                table: "Car",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CarModel_BrandID",
                table: "CarModel",
                column: "BrandID");

            migrationBuilder.CreateIndex(
                name: "IX_Car_BrandID",
                table: "Car",
                column: "BrandID");

            migrationBuilder.AddForeignKey(
                name: "FK_Car_Brand_BrandID",
                table: "Car",
                column: "BrandID",
                principalTable: "Brand",
                principalColumn: "ID",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_CarModel_Brand_BrandID",
                table: "CarModel",
                column: "BrandID",
                principalTable: "Brand",
                principalColumn: "ID",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Car_Brand_BrandID",
                table: "Car");

            migrationBuilder.DropForeignKey(
                name: "FK_CarModel_Brand_BrandID",
                table: "CarModel");

            migrationBuilder.DropIndex(
                name: "IX_CarModel_BrandID",
                table: "CarModel");

            migrationBuilder.DropIndex(
                name: "IX_Car_BrandID",
                table: "Car");

            migrationBuilder.DropColumn(
                name: "BrandID",
                table: "CarModel");

            migrationBuilder.DropColumn(
                name: "NazivModela",
                table: "CarModel");

            migrationBuilder.DropColumn(
                name: "BrandID",
                table: "Car");

            migrationBuilder.AddColumn<string>(
                name: "CarModelName",
                table: "CarModel",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Brand",
                table: "Car",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
