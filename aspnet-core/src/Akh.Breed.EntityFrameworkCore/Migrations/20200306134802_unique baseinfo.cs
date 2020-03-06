using Microsoft.EntityFrameworkCore.Migrations;

namespace Akh.Breed.Migrations
{
    public partial class uniquebaseinfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AkhStateInfo_Code",
                table: "AkhStateInfo");

            migrationBuilder.DropIndex(
                name: "IX_AkhStateInfo_Name",
                table: "AkhStateInfo");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AkhVillageInfo",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "AkhVillageInfo",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AkhStateInfo",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "AkhStateInfo",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AkhSpeciesInfo",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "AkhSpeciesInfo",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AkhSexInfo",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "AkhSexInfo",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AkhRegionInfo",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "AkhRegionInfo",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AkhProviderInfo",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "AkhProviderInfo",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AkhPlaqueState",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "AkhPlaqueState",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AkhFirmType",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "AkhFirmType",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AkhCityInfo",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "AkhCityInfo",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AkhAcademicDegree",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "AkhAcademicDegree",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AkhVillageInfo_Code_RegionInfoId",
                table: "AkhVillageInfo",
                columns: new[] { "Code", "RegionInfoId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AkhVillageInfo_Name_RegionInfoId",
                table: "AkhVillageInfo",
                columns: new[] { "Name", "RegionInfoId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AkhStateInfo_Code",
                table: "AkhStateInfo",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AkhStateInfo_Name",
                table: "AkhStateInfo",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AkhSpeciesInfo_Code",
                table: "AkhSpeciesInfo",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AkhSpeciesInfo_Name",
                table: "AkhSpeciesInfo",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AkhSexInfo_Code",
                table: "AkhSexInfo",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AkhSexInfo_Name",
                table: "AkhSexInfo",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AkhRegionInfo_Code_CityInfoId",
                table: "AkhRegionInfo",
                columns: new[] { "Code", "CityInfoId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AkhRegionInfo_Name_CityInfoId",
                table: "AkhRegionInfo",
                columns: new[] { "Name", "CityInfoId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AkhProviderInfo_Code",
                table: "AkhProviderInfo",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AkhProviderInfo_Name",
                table: "AkhProviderInfo",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AkhPlaqueState_Code",
                table: "AkhPlaqueState",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AkhPlaqueState_Name",
                table: "AkhPlaqueState",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AkhFirmType_Code",
                table: "AkhFirmType",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AkhFirmType_Name",
                table: "AkhFirmType",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AkhCityInfo_Code_StateInfoId",
                table: "AkhCityInfo",
                columns: new[] { "Code", "StateInfoId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AkhCityInfo_Name_StateInfoId",
                table: "AkhCityInfo",
                columns: new[] { "Name", "StateInfoId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AkhAcademicDegree_Code",
                table: "AkhAcademicDegree",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AkhAcademicDegree_Name",
                table: "AkhAcademicDegree",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AkhVillageInfo_Code_RegionInfoId",
                table: "AkhVillageInfo");

            migrationBuilder.DropIndex(
                name: "IX_AkhVillageInfo_Name_RegionInfoId",
                table: "AkhVillageInfo");

            migrationBuilder.DropIndex(
                name: "IX_AkhStateInfo_Code",
                table: "AkhStateInfo");

            migrationBuilder.DropIndex(
                name: "IX_AkhStateInfo_Name",
                table: "AkhStateInfo");

            migrationBuilder.DropIndex(
                name: "IX_AkhSpeciesInfo_Code",
                table: "AkhSpeciesInfo");

            migrationBuilder.DropIndex(
                name: "IX_AkhSpeciesInfo_Name",
                table: "AkhSpeciesInfo");

            migrationBuilder.DropIndex(
                name: "IX_AkhSexInfo_Code",
                table: "AkhSexInfo");

            migrationBuilder.DropIndex(
                name: "IX_AkhSexInfo_Name",
                table: "AkhSexInfo");

            migrationBuilder.DropIndex(
                name: "IX_AkhRegionInfo_Code_CityInfoId",
                table: "AkhRegionInfo");

            migrationBuilder.DropIndex(
                name: "IX_AkhRegionInfo_Name_CityInfoId",
                table: "AkhRegionInfo");

            migrationBuilder.DropIndex(
                name: "IX_AkhProviderInfo_Code",
                table: "AkhProviderInfo");

            migrationBuilder.DropIndex(
                name: "IX_AkhProviderInfo_Name",
                table: "AkhProviderInfo");

            migrationBuilder.DropIndex(
                name: "IX_AkhPlaqueState_Code",
                table: "AkhPlaqueState");

            migrationBuilder.DropIndex(
                name: "IX_AkhPlaqueState_Name",
                table: "AkhPlaqueState");

            migrationBuilder.DropIndex(
                name: "IX_AkhFirmType_Code",
                table: "AkhFirmType");

            migrationBuilder.DropIndex(
                name: "IX_AkhFirmType_Name",
                table: "AkhFirmType");

            migrationBuilder.DropIndex(
                name: "IX_AkhCityInfo_Code_StateInfoId",
                table: "AkhCityInfo");

            migrationBuilder.DropIndex(
                name: "IX_AkhCityInfo_Name_StateInfoId",
                table: "AkhCityInfo");

            migrationBuilder.DropIndex(
                name: "IX_AkhAcademicDegree_Code",
                table: "AkhAcademicDegree");

            migrationBuilder.DropIndex(
                name: "IX_AkhAcademicDegree_Name",
                table: "AkhAcademicDegree");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AkhVillageInfo",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "AkhVillageInfo",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AkhStateInfo",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "AkhStateInfo",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AkhSpeciesInfo",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "AkhSpeciesInfo",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AkhSexInfo",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "AkhSexInfo",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AkhRegionInfo",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "AkhRegionInfo",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AkhProviderInfo",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "AkhProviderInfo",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AkhPlaqueState",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "AkhPlaqueState",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AkhFirmType",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "AkhFirmType",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AkhCityInfo",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "AkhCityInfo",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AkhAcademicDegree",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "AkhAcademicDegree",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.CreateIndex(
                name: "IX_AkhStateInfo_Code",
                table: "AkhStateInfo",
                column: "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AkhStateInfo_Name",
                table: "AkhStateInfo",
                column: "Name",
                unique: true,
                filter: "[Name] IS NOT NULL");
        }
    }
}
