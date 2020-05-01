using Microsoft.EntityFrameworkCore.Migrations;

namespace Akh.Breed.Migrations
{
    public partial class addManufacturertoplaqueStore : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ManufacturerId",
                table: "AkhPlaqueStores",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AkhPlaqueStores_ManufacturerId",
                table: "AkhPlaqueStores",
                column: "ManufacturerId");

            migrationBuilder.AddForeignKey(
                name: "FK_AkhPlaqueStores_AkhManufacturers_ManufacturerId",
                table: "AkhPlaqueStores",
                column: "ManufacturerId",
                principalTable: "AkhManufacturers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AkhPlaqueStores_AkhManufacturers_ManufacturerId",
                table: "AkhPlaqueStores");

            migrationBuilder.DropIndex(
                name: "IX_AkhPlaqueStores_ManufacturerId",
                table: "AkhPlaqueStores");

            migrationBuilder.DropColumn(
                name: "ManufacturerId",
                table: "AkhPlaqueStores");
        }
    }
}
