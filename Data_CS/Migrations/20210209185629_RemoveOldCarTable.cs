using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data_CS.Migrations
{
    public partial class RemoveOldCarTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Car");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Car",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BbrandModelID = table.Column<int>(type: "int", nullable: true),
                    BrandModelID = table.Column<int>(type: "int", nullable: false),
                    Ccm = table.Column<float>(type: "real", nullable: false),
                    ColorID = table.Column<int>(type: "int", nullable: false),
                    DateOfManufacture = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DriveTypeID = table.Column<int>(type: "int", nullable: false),
                    FuelID = table.Column<int>(type: "int", nullable: false),
                    Kilometre = table.Column<float>(type: "real", nullable: false),
                    NumberOfDors = table.Column<int>(type: "int", nullable: false),
                    NumberOfGears = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumberOfSeats = table.Column<int>(type: "int", nullable: false),
                    PowerKw = table.Column<int>(type: "int", nullable: false),
                    PowerPS = table.Column<int>(type: "int", nullable: false),
                    TransmissionID = table.Column<int>(type: "int", nullable: false),
                    VehicleTypeID = table.Column<int>(type: "int", nullable: false),
                    WheelSize = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Car", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Car_Brand_Model_BbrandModelID",
                        column: x => x.BbrandModelID,
                        principalTable: "Brand_Model",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Car_Color_ColorID",
                        column: x => x.ColorID,
                        principalTable: "Color",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Car_DriveType_DriveTypeID",
                        column: x => x.DriveTypeID,
                        principalTable: "DriveType",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Car_Fuel_FuelID",
                        column: x => x.FuelID,
                        principalTable: "Fuel",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Car_Transmission_TransmissionID",
                        column: x => x.TransmissionID,
                        principalTable: "Transmission",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Car_VehicleType_VehicleTypeID",
                        column: x => x.VehicleTypeID,
                        principalTable: "VehicleType",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Car_BbrandModelID",
                table: "Car",
                column: "BbrandModelID");

            migrationBuilder.CreateIndex(
                name: "IX_Car_ColorID",
                table: "Car",
                column: "ColorID");

            migrationBuilder.CreateIndex(
                name: "IX_Car_DriveTypeID",
                table: "Car",
                column: "DriveTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Car_FuelID",
                table: "Car",
                column: "FuelID");

            migrationBuilder.CreateIndex(
                name: "IX_Car_TransmissionID",
                table: "Car",
                column: "TransmissionID");

            migrationBuilder.CreateIndex(
                name: "IX_Car_VehicleTypeID",
                table: "Car",
                column: "VehicleTypeID");
        }
    }
}
