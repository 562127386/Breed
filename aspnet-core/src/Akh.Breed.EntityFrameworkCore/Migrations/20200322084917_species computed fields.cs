using Microsoft.EntityFrameworkCore.Migrations;

namespace Akh.Breed.Migrations
{
    public partial class speciescomputedfields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "FromCode",
                table: "AkhSpeciesInfo",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "FromCodeStr",
                table: "AkhSpeciesInfo",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ToCode",
                table: "AkhSpeciesInfo",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "ToCodeStr",
                table: "AkhSpeciesInfo",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FromCode",
                table: "AkhSpeciesInfo");

            migrationBuilder.DropColumn(
                name: "FromCodeStr",
                table: "AkhSpeciesInfo");

            migrationBuilder.DropColumn(
                name: "ToCode",
                table: "AkhSpeciesInfo");

            migrationBuilder.DropColumn(
                name: "ToCodeStr",
                table: "AkhSpeciesInfo");
        }
    }
}
