using Microsoft.EntityFrameworkCore.Migrations;

namespace Data_CS.Migrations
{
    public partial class Wishlist : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserWishlist",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    brand = table.Column<string>(nullable: true),
                    model = table.Column<string>(nullable: true),
                    fuel = table.Column<string>(nullable: true),
                    color = table.Column<string>(nullable: true),
                    numberOfGears = table.Column<string>(nullable: true),
                    powerKw = table.Column<int>(nullable: false),
                    wheelSize = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserWishlist", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserWishlist");
        }
    }
}
