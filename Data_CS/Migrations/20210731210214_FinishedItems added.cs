using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data_CS.Migrations
{
    public partial class FinishedItemsadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FinishedItems",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BrandID = table.Column<int>(nullable: false),
                    Model = table.Column<string>(nullable: true),
                    FuelID = table.Column<int>(nullable: false),
                    VehicleTypeID = table.Column<int>(nullable: false),
                    ColorID = table.Column<int>(nullable: false),
                    DriveTypeID = table.Column<int>(nullable: false),
                    TransmissionID = table.Column<int>(nullable: false),
                    NumberOfSeats = table.Column<int>(nullable: false),
                    NumberOfDors = table.Column<int>(nullable: false),
                    NumberOfGears = table.Column<string>(nullable: true),
                    PowerPS = table.Column<int>(nullable: false),
                    PowerKw = table.Column<int>(nullable: false),
                    WheelSize = table.Column<float>(nullable: false),
                    Ccm = table.Column<float>(nullable: false),
                    Kilometre = table.Column<float>(nullable: false),
                    DateOfManufacture = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinishedItems", x => x.ID);
                    table.ForeignKey(
                        name: "FK_FinishedItems_Brand_BrandID",
                        column: x => x.BrandID,
                        principalTable: "Brand",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FinishedItems_Color_ColorID",
                        column: x => x.ColorID,
                        principalTable: "Color",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FinishedItems_DriveType_DriveTypeID",
                        column: x => x.DriveTypeID,
                        principalTable: "DriveType",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FinishedItems_Fuel_FuelID",
                        column: x => x.FuelID,
                        principalTable: "Fuel",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FinishedItems_Transmission_TransmissionID",
                        column: x => x.TransmissionID,
                        principalTable: "Transmission",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FinishedItems_VehicleType_VehicleTypeID",
                        column: x => x.VehicleTypeID,
                        principalTable: "VehicleType",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FinishedItems_BrandID",
                table: "FinishedItems",
                column: "BrandID");

            migrationBuilder.CreateIndex(
                name: "IX_FinishedItems_ColorID",
                table: "FinishedItems",
                column: "ColorID");

            migrationBuilder.CreateIndex(
                name: "IX_FinishedItems_DriveTypeID",
                table: "FinishedItems",
                column: "DriveTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_FinishedItems_FuelID",
                table: "FinishedItems",
                column: "FuelID");

            migrationBuilder.CreateIndex(
                name: "IX_FinishedItems_TransmissionID",
                table: "FinishedItems",
                column: "TransmissionID");

            migrationBuilder.CreateIndex(
                name: "IX_FinishedItems_VehicleTypeID",
                table: "FinishedItems",
                column: "VehicleTypeID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FinishedItems");
        }
    }
}
