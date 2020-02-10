using Microsoft.EntityFrameworkCore.Migrations;

namespace Akh.Breed.Migrations
{
    public partial class changecityinfotable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AkhVillageInfo_AkhCityInfo_CityInfoId",
                table: "AkhVillageInfo");

            migrationBuilder.AlterColumn<int>(
                name: "CityInfoId",
                table: "AkhVillageInfo",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AkhVillageInfo_AkhCityInfo_CityInfoId",
                table: "AkhVillageInfo",
                column: "CityInfoId",
                principalTable: "AkhCityInfo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AkhVillageInfo_AkhCityInfo_CityInfoId",
                table: "AkhVillageInfo");

            migrationBuilder.AlterColumn<int>(
                name: "CityInfoId",
                table: "AkhVillageInfo",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_AkhVillageInfo_AkhCityInfo_CityInfoId",
                table: "AkhVillageInfo",
                column: "CityInfoId",
                principalTable: "AkhCityInfo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
