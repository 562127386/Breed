using Microsoft.EntityFrameworkCore.Migrations;

namespace Akh.Breed.Migrations
{
    public partial class plaquecountadd2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AkhPlaqueInfos_AkhPlaqueStores_PlaqueStoreId",
                table: "AkhPlaqueInfos");

            migrationBuilder.DropIndex(
                name: "IX_AkhPlaqueInfos_PlaqueStoreId",
                table: "AkhPlaqueInfos");

            migrationBuilder.DropColumn(
                name: "PlaqueStoreId",
                table: "AkhPlaqueInfos");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PlaqueStoreId",
                table: "AkhPlaqueInfos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AkhPlaqueInfos_PlaqueStoreId",
                table: "AkhPlaqueInfos",
                column: "PlaqueStoreId");

            migrationBuilder.AddForeignKey(
                name: "FK_AkhPlaqueInfos_AkhPlaqueStores_PlaqueStoreId",
                table: "AkhPlaqueInfos",
                column: "PlaqueStoreId",
                principalTable: "AkhPlaqueStores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
