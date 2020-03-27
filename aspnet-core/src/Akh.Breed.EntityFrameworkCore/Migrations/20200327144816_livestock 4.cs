using Microsoft.EntityFrameworkCore.Migrations;

namespace Akh.Breed.Migrations
{
    public partial class livestock4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LivestockId",
                table: "AkhPlaqueInfos",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AkhPlaqueInfos_LivestockId",
                table: "AkhPlaqueInfos",
                column: "LivestockId");

            migrationBuilder.AddForeignKey(
                name: "FK_AkhPlaqueInfos_AkhLivestocks_LivestockId",
                table: "AkhPlaqueInfos",
                column: "LivestockId",
                principalTable: "AkhLivestocks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AkhPlaqueInfos_AkhLivestocks_LivestockId",
                table: "AkhPlaqueInfos");

            migrationBuilder.DropIndex(
                name: "IX_AkhPlaqueInfos_LivestockId",
                table: "AkhPlaqueInfos");

            migrationBuilder.DropColumn(
                name: "LivestockId",
                table: "AkhPlaqueInfos");
        }
    }
}
