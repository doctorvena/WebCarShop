using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data_CS.Migrations
{
    public partial class ser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ServicedCars",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServiceName = table.Column<string>(nullable: true),
                    DateOfServiced = table.Column<DateTime>(nullable: false),
                    DateofServiceWarranty = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Recommendations = table.Column<string>(nullable: true),
                    Warnings = table.Column<string>(nullable: true),
                    Price = table.Column<float>(nullable: false),
                    CarID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServicedCars", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ServicedCars_Car_CarID",
                        column: x => x.CarID,
                        principalTable: "Car",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ServicedCars_CarID",
                table: "ServicedCars",
                column: "CarID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ServicedCars");
        }
    }
}
