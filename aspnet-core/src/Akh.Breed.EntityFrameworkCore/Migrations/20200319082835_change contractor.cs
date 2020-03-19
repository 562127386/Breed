using Microsoft.EntityFrameworkCore.Migrations;

namespace Akh.Breed.Migrations
{
    public partial class changecontractor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UnionInfoId",
                table: "AkhContractors",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "VillageInfoId",
                table: "AkhContractors",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_AkhContractors_UnionInfoId",
                table: "AkhContractors",
                column: "UnionInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_AkhContractors_VillageInfoId",
                table: "AkhContractors",
                column: "VillageInfoId");

            migrationBuilder.AddForeignKey(
                name: "FK_AkhContractors_AkhUnionInfo_UnionInfoId",
                table: "AkhContractors",
                column: "UnionInfoId",
                principalTable: "AkhUnionInfo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AkhContractors_AkhVillageInfo_VillageInfoId",
                table: "AkhContractors",
                column: "VillageInfoId",
                principalTable: "AkhVillageInfo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AkhContractors_AkhUnionInfo_UnionInfoId",
                table: "AkhContractors");

            migrationBuilder.DropForeignKey(
                name: "FK_AkhContractors_AkhVillageInfo_VillageInfoId",
                table: "AkhContractors");

            migrationBuilder.DropIndex(
                name: "IX_AkhContractors_UnionInfoId",
                table: "AkhContractors");

            migrationBuilder.DropIndex(
                name: "IX_AkhContractors_VillageInfoId",
                table: "AkhContractors");

            migrationBuilder.DropColumn(
                name: "UnionInfoId",
                table: "AkhContractors");

            migrationBuilder.DropColumn(
                name: "VillageInfoId",
                table: "AkhContractors");
        }
    }
}
