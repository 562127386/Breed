using Microsoft.EntityFrameworkCore.Migrations;

namespace Akh.Breed.Migrations
{
    public partial class changeunion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "AkhUnionInfo",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Family",
                table: "AkhUnionInfo",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "AkhUnionInfo",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NationalCode",
                table: "AkhUnionInfo",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "AkhUnionInfo",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PostalCode",
                table: "AkhUnionInfo",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "AkhUnionInfo");

            migrationBuilder.DropColumn(
                name: "Family",
                table: "AkhUnionInfo");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "AkhUnionInfo");

            migrationBuilder.DropColumn(
                name: "NationalCode",
                table: "AkhUnionInfo");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "AkhUnionInfo");

            migrationBuilder.DropColumn(
                name: "PostalCode",
                table: "AkhUnionInfo");
        }
    }
}
