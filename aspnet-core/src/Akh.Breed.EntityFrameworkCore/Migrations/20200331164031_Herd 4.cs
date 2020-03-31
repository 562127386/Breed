using Microsoft.EntityFrameworkCore.Migrations;

namespace Akh.Breed.Migrations
{
    public partial class Herd4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Capacity",
                table: "AkhHerds",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "FirmCode",
                table: "AkhHerds",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirmName",
                table: "AkhHerds",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Capacity",
                table: "AkhHerds");

            migrationBuilder.DropColumn(
                name: "FirmCode",
                table: "AkhHerds");

            migrationBuilder.DropColumn(
                name: "FirmName",
                table: "AkhHerds");
        }
    }
}
