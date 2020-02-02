using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Akh.Breed.Migrations
{
    public partial class addBaseInfo2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AkhAcademicDegree",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    TenantId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AkhAcademicDegree", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AkhFirmType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    TenantId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AkhFirmType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AkhPlaqueState",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    TenantId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AkhPlaqueState", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AkhProviderInfo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    TenantId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AkhProviderInfo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AkhSexInfo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    TenantId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AkhSexInfo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AkhSpeciesInfo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    TenantId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AkhSpeciesInfo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AkhStateInfo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    TenantId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AkhStateInfo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AkhCityInfo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    TenantId = table.Column<int>(nullable: true),
                    StateInfoId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AkhCityInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AkhCityInfo_AkhStateInfo_StateInfoId",
                        column: x => x.StateInfoId,
                        principalTable: "AkhStateInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AkhVillageInfo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    TenantId = table.Column<int>(nullable: true),
                    CityInfoId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AkhVillageInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AkhVillageInfo_AkhCityInfo_CityInfoId",
                        column: x => x.CityInfoId,
                        principalTable: "AkhCityInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AkhCityInfo_StateInfoId",
                table: "AkhCityInfo",
                column: "StateInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_AkhVillageInfo_CityInfoId",
                table: "AkhVillageInfo",
                column: "CityInfoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AkhAcademicDegree");

            migrationBuilder.DropTable(
                name: "AkhFirmType");

            migrationBuilder.DropTable(
                name: "AkhPlaqueState");

            migrationBuilder.DropTable(
                name: "AkhProviderInfo");

            migrationBuilder.DropTable(
                name: "AkhSexInfo");

            migrationBuilder.DropTable(
                name: "AkhSpeciesInfo");

            migrationBuilder.DropTable(
                name: "AkhVillageInfo");

            migrationBuilder.DropTable(
                name: "AkhCityInfo");

            migrationBuilder.DropTable(
                name: "AkhStateInfo");
        }
    }
}
