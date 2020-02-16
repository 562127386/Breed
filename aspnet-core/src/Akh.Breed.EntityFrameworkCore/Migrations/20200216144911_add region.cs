using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Akh.Breed.Migrations
{
    public partial class addregion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AkhVillageInfo_AkhCityInfo_CityInfoId",
                table: "AkhVillageInfo");

            migrationBuilder.AlterColumn<int>(
                name: "CityInfoId",
                table: "AkhVillageInfo",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "RegionInfoId",
                table: "AkhVillageInfo",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "AkhRegionInfo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    TenantId = table.Column<int>(nullable: true),
                    CityInfoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AkhRegionInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AkhRegionInfo_AkhCityInfo_CityInfoId",
                        column: x => x.CityInfoId,
                        principalTable: "AkhCityInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AkhVillageInfo_RegionInfoId",
                table: "AkhVillageInfo",
                column: "RegionInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_AkhRegionInfo_CityInfoId",
                table: "AkhRegionInfo",
                column: "CityInfoId");

            migrationBuilder.AddForeignKey(
                name: "FK_AkhVillageInfo_AkhCityInfo_CityInfoId",
                table: "AkhVillageInfo",
                column: "CityInfoId",
                principalTable: "AkhCityInfo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AkhVillageInfo_AkhRegionInfo_RegionInfoId",
                table: "AkhVillageInfo",
                column: "RegionInfoId",
                principalTable: "AkhRegionInfo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AkhVillageInfo_AkhCityInfo_CityInfoId",
                table: "AkhVillageInfo");

            migrationBuilder.DropForeignKey(
                name: "FK_AkhVillageInfo_AkhRegionInfo_RegionInfoId",
                table: "AkhVillageInfo");

            migrationBuilder.DropTable(
                name: "AkhRegionInfo");

            migrationBuilder.DropIndex(
                name: "IX_AkhVillageInfo_RegionInfoId",
                table: "AkhVillageInfo");

            migrationBuilder.DropColumn(
                name: "RegionInfoId",
                table: "AkhVillageInfo");

            migrationBuilder.AlterColumn<int>(
                name: "CityInfoId",
                table: "AkhVillageInfo",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AkhVillageInfo_AkhCityInfo_CityInfoId",
                table: "AkhVillageInfo",
                column: "CityInfoId",
                principalTable: "AkhCityInfo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
