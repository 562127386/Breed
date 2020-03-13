using Microsoft.EntityFrameworkCore.Migrations;

namespace Akh.Breed.Migrations
{
    public partial class plaquecountadd3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PlaqueCount",
                table: "AkhPlaqueStores");

            migrationBuilder.DropColumn(
                name: "PlaqueCount",
                table: "AkhPlaqueOfficers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PlaqueCount",
                table: "AkhPlaqueStores",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PlaqueCount",
                table: "AkhPlaqueOfficers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
