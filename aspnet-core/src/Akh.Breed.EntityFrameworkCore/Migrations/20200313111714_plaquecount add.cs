using Microsoft.EntityFrameworkCore.Migrations;

namespace Akh.Breed.Migrations
{
    public partial class plaquecountadd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PlaqueCount",
                table: "AkhPlaqueStores",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PlaqueCount",
                table: "AkhPlaqueOfficers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PlaqueStoreId",
                table: "AkhPlaqueInfos",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AkhPlaqueStores_FromCode",
                table: "AkhPlaqueStores",
                column: "FromCode");

            migrationBuilder.CreateIndex(
                name: "IX_AkhPlaqueStores_ToCode",
                table: "AkhPlaqueStores",
                column: "ToCode");

            migrationBuilder.CreateIndex(
                name: "IX_AkhPlaqueOfficers_FromCode",
                table: "AkhPlaqueOfficers",
                column: "FromCode");

            migrationBuilder.CreateIndex(
                name: "IX_AkhPlaqueOfficers_ToCode",
                table: "AkhPlaqueOfficers",
                column: "ToCode");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AkhPlaqueInfos_AkhPlaqueStores_PlaqueStoreId",
                table: "AkhPlaqueInfos");

            migrationBuilder.DropIndex(
                name: "IX_AkhPlaqueStores_FromCode",
                table: "AkhPlaqueStores");

            migrationBuilder.DropIndex(
                name: "IX_AkhPlaqueStores_ToCode",
                table: "AkhPlaqueStores");

            migrationBuilder.DropIndex(
                name: "IX_AkhPlaqueOfficers_FromCode",
                table: "AkhPlaqueOfficers");

            migrationBuilder.DropIndex(
                name: "IX_AkhPlaqueOfficers_ToCode",
                table: "AkhPlaqueOfficers");

            migrationBuilder.DropIndex(
                name: "IX_AkhPlaqueInfos_PlaqueStoreId",
                table: "AkhPlaqueInfos");

            migrationBuilder.DropColumn(
                name: "PlaqueCount",
                table: "AkhPlaqueStores");

            migrationBuilder.DropColumn(
                name: "PlaqueCount",
                table: "AkhPlaqueOfficers");

            migrationBuilder.DropColumn(
                name: "PlaqueStoreId",
                table: "AkhPlaqueInfos");
        }
    }
}
