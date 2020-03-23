using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Akh.Breed.Migrations
{
    public partial class addherd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AkhContractors_AkhFirmType_FirmTypeId",
                table: "AkhContractors");

            migrationBuilder.DropForeignKey(
                name: "FK_AkhContractors_AkhUnionInfo_UnionInfoId",
                table: "AkhContractors");

            migrationBuilder.DropForeignKey(
                name: "FK_AkhContractors_AkhVillageInfo_VillageInfoId",
                table: "AkhContractors");

            migrationBuilder.AlterColumn<int>(
                name: "VillageInfoId",
                table: "AkhContractors",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "UnionInfoId",
                table: "AkhContractors",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "FirmTypeId",
                table: "AkhContractors",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "AkhEpidemiologicInfo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Family = table.Column<string>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    TenantId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AkhEpidemiologicInfo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AkhHerds",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HerdName = table.Column<string>(nullable: false),
                    Latitude = table.Column<string>(nullable: true),
                    Longitude = table.Column<string>(nullable: true),
                    AgriculturalId = table.Column<string>(nullable: false),
                    ActivityStatus = table.Column<bool>(nullable: false),
                    LicenseStatus = table.Column<bool>(nullable: false),
                    LicenseNum = table.Column<string>(nullable: false),
                    IssueDate = table.Column<DateTime>(nullable: true),
                    ValidityDate = table.Column<DateTime>(nullable: true),
                    Iranian = table.Column<bool>(nullable: false),
                    Reality = table.Column<bool>(nullable: false),
                    Code = table.Column<string>(nullable: true),
                    NationalCode = table.Column<string>(nullable: false),
                    BirthDate = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Family = table.Column<string>(nullable: false),
                    Mobile = table.Column<string>(nullable: false),
                    Phone = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    EpidemiologicInfoId = table.Column<int>(nullable: true),
                    UnionInfoId = table.Column<int>(nullable: true),
                    VillageInfoId = table.Column<int>(nullable: true),
                    ActivityInfoId = table.Column<int>(nullable: true),
                    ContractorId = table.Column<int>(nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    TenantId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AkhHerds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AkhHerds_AkhActivityInfo_ActivityInfoId",
                        column: x => x.ActivityInfoId,
                        principalTable: "AkhActivityInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AkhHerds_AkhContractors_ContractorId",
                        column: x => x.ContractorId,
                        principalTable: "AkhContractors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AkhHerds_AkhEpidemiologicInfo_EpidemiologicInfoId",
                        column: x => x.EpidemiologicInfoId,
                        principalTable: "AkhEpidemiologicInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AkhHerds_AkhUnionInfo_UnionInfoId",
                        column: x => x.UnionInfoId,
                        principalTable: "AkhUnionInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AkhHerds_AkhVillageInfo_VillageInfoId",
                        column: x => x.VillageInfoId,
                        principalTable: "AkhVillageInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AkhEpidemiologicInfo_Code",
                table: "AkhEpidemiologicInfo",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AkhHerds_ActivityInfoId",
                table: "AkhHerds",
                column: "ActivityInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_AkhHerds_ContractorId",
                table: "AkhHerds",
                column: "ContractorId");

            migrationBuilder.CreateIndex(
                name: "IX_AkhHerds_EpidemiologicInfoId",
                table: "AkhHerds",
                column: "EpidemiologicInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_AkhHerds_UnionInfoId",
                table: "AkhHerds",
                column: "UnionInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_AkhHerds_VillageInfoId",
                table: "AkhHerds",
                column: "VillageInfoId");

            migrationBuilder.AddForeignKey(
                name: "FK_AkhContractors_AkhFirmType_FirmTypeId",
                table: "AkhContractors",
                column: "FirmTypeId",
                principalTable: "AkhFirmType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AkhContractors_AkhUnionInfo_UnionInfoId",
                table: "AkhContractors",
                column: "UnionInfoId",
                principalTable: "AkhUnionInfo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AkhContractors_AkhVillageInfo_VillageInfoId",
                table: "AkhContractors",
                column: "VillageInfoId",
                principalTable: "AkhVillageInfo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AkhContractors_AkhFirmType_FirmTypeId",
                table: "AkhContractors");

            migrationBuilder.DropForeignKey(
                name: "FK_AkhContractors_AkhUnionInfo_UnionInfoId",
                table: "AkhContractors");

            migrationBuilder.DropForeignKey(
                name: "FK_AkhContractors_AkhVillageInfo_VillageInfoId",
                table: "AkhContractors");

            migrationBuilder.DropTable(
                name: "AkhHerds");

            migrationBuilder.DropTable(
                name: "AkhEpidemiologicInfo");

            migrationBuilder.AlterColumn<int>(
                name: "VillageInfoId",
                table: "AkhContractors",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UnionInfoId",
                table: "AkhContractors",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "FirmTypeId",
                table: "AkhContractors",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AkhContractors_AkhFirmType_FirmTypeId",
                table: "AkhContractors",
                column: "FirmTypeId",
                principalTable: "AkhFirmType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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
    }
}
