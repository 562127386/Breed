using Microsoft.EntityFrameworkCore.Migrations;

namespace Akh.Breed.Migrations
{
    public partial class contractorupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CityInfoId",
                table: "AkhContractors",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RegionInfoId",
                table: "AkhContractors",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StateInfoId",
                table: "AkhContractors",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AkhContractors_CityInfoId",
                table: "AkhContractors",
                column: "CityInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_AkhContractors_RegionInfoId",
                table: "AkhContractors",
                column: "RegionInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_AkhContractors_StateInfoId",
                table: "AkhContractors",
                column: "StateInfoId");

            migrationBuilder.AddForeignKey(
                name: "FK_AkhContractors_AkhCityInfo_CityInfoId",
                table: "AkhContractors",
                column: "CityInfoId",
                principalTable: "AkhCityInfo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AkhContractors_AkhRegionInfo_RegionInfoId",
                table: "AkhContractors",
                column: "RegionInfoId",
                principalTable: "AkhRegionInfo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AkhContractors_AkhStateInfo_StateInfoId",
                table: "AkhContractors",
                column: "StateInfoId",
                principalTable: "AkhStateInfo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AkhContractors_AkhCityInfo_CityInfoId",
                table: "AkhContractors");

            migrationBuilder.DropForeignKey(
                name: "FK_AkhContractors_AkhRegionInfo_RegionInfoId",
                table: "AkhContractors");

            migrationBuilder.DropForeignKey(
                name: "FK_AkhContractors_AkhStateInfo_StateInfoId",
                table: "AkhContractors");

            migrationBuilder.DropIndex(
                name: "IX_AkhContractors_CityInfoId",
                table: "AkhContractors");

            migrationBuilder.DropIndex(
                name: "IX_AkhContractors_RegionInfoId",
                table: "AkhContractors");

            migrationBuilder.DropIndex(
                name: "IX_AkhContractors_StateInfoId",
                table: "AkhContractors");

            migrationBuilder.DropColumn(
                name: "CityInfoId",
                table: "AkhContractors");

            migrationBuilder.DropColumn(
                name: "RegionInfoId",
                table: "AkhContractors");

            migrationBuilder.DropColumn(
                name: "StateInfoId",
                table: "AkhContractors");
        }
    }
}
