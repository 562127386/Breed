using Microsoft.EntityFrameworkCore.Migrations;

namespace Akh.Breed.Migrations
{
    public partial class herdupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CityInfoId",
                table: "AkhHerds",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EpidemiologicCode",
                table: "AkhHerds",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RegionInfoId",
                table: "AkhHerds",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StateInfoId",
                table: "AkhHerds",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AkhHerds_CityInfoId",
                table: "AkhHerds",
                column: "CityInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_AkhHerds_RegionInfoId",
                table: "AkhHerds",
                column: "RegionInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_AkhHerds_StateInfoId",
                table: "AkhHerds",
                column: "StateInfoId");

            migrationBuilder.AddForeignKey(
                name: "FK_AkhHerds_AkhCityInfo_CityInfoId",
                table: "AkhHerds",
                column: "CityInfoId",
                principalTable: "AkhCityInfo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AkhHerds_AkhRegionInfo_RegionInfoId",
                table: "AkhHerds",
                column: "RegionInfoId",
                principalTable: "AkhRegionInfo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AkhHerds_AkhStateInfo_StateInfoId",
                table: "AkhHerds",
                column: "StateInfoId",
                principalTable: "AkhStateInfo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AkhHerds_AkhCityInfo_CityInfoId",
                table: "AkhHerds");

            migrationBuilder.DropForeignKey(
                name: "FK_AkhHerds_AkhRegionInfo_RegionInfoId",
                table: "AkhHerds");

            migrationBuilder.DropForeignKey(
                name: "FK_AkhHerds_AkhStateInfo_StateInfoId",
                table: "AkhHerds");

            migrationBuilder.DropIndex(
                name: "IX_AkhHerds_CityInfoId",
                table: "AkhHerds");

            migrationBuilder.DropIndex(
                name: "IX_AkhHerds_RegionInfoId",
                table: "AkhHerds");

            migrationBuilder.DropIndex(
                name: "IX_AkhHerds_StateInfoId",
                table: "AkhHerds");

            migrationBuilder.DropColumn(
                name: "CityInfoId",
                table: "AkhHerds");

            migrationBuilder.DropColumn(
                name: "EpidemiologicCode",
                table: "AkhHerds");

            migrationBuilder.DropColumn(
                name: "RegionInfoId",
                table: "AkhHerds");

            migrationBuilder.DropColumn(
                name: "StateInfoId",
                table: "AkhHerds");
        }
    }
}
