using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Akh.Breed.Migrations
{
    public partial class plaquetoContractor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AkhPlaqueToOfficers_AkhPlaqueToCities_PlaqueToCityId",
                table: "AkhPlaqueToOfficers");

            migrationBuilder.DropTable(
                name: "AkhPlaqueToCities");

            migrationBuilder.DropIndex(
                name: "IX_AkhPlaqueToOfficers_PlaqueToCityId",
                table: "AkhPlaqueToOfficers");

            migrationBuilder.DropColumn(
                name: "PlaqueToCityId",
                table: "AkhPlaqueToOfficers");

            migrationBuilder.AddColumn<int>(
                name: "PlaqueToContractorId",
                table: "AkhPlaqueToOfficers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AkhPlaqueToContractors",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FromCode = table.Column<long>(maxLength: 15, nullable: false),
                    ToCode = table.Column<long>(maxLength: 15, nullable: false),
                    LastCode = table.Column<long>(maxLength: 15, nullable: false),
                    SetTime = table.Column<DateTime>(nullable: false),
                    ContractorId = table.Column<int>(nullable: true),
                    PlaqueToStateId = table.Column<int>(nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    TenantId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AkhPlaqueToContractors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AkhPlaqueToContractors_AkhContractors_ContractorId",
                        column: x => x.ContractorId,
                        principalTable: "AkhContractors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AkhPlaqueToContractors_AkhPlaqueToStates_PlaqueToStateId",
                        column: x => x.PlaqueToStateId,
                        principalTable: "AkhPlaqueToStates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AkhPlaqueToOfficers_PlaqueToContractorId",
                table: "AkhPlaqueToOfficers",
                column: "PlaqueToContractorId");

            migrationBuilder.CreateIndex(
                name: "IX_AkhPlaqueToContractors_ContractorId",
                table: "AkhPlaqueToContractors",
                column: "ContractorId");

            migrationBuilder.CreateIndex(
                name: "IX_AkhPlaqueToContractors_FromCode",
                table: "AkhPlaqueToContractors",
                column: "FromCode");

            migrationBuilder.CreateIndex(
                name: "IX_AkhPlaqueToContractors_PlaqueToStateId",
                table: "AkhPlaqueToContractors",
                column: "PlaqueToStateId");

            migrationBuilder.CreateIndex(
                name: "IX_AkhPlaqueToContractors_ToCode",
                table: "AkhPlaqueToContractors",
                column: "ToCode");

            migrationBuilder.AddForeignKey(
                name: "FK_AkhPlaqueToOfficers_AkhPlaqueToContractors_PlaqueToContractorId",
                table: "AkhPlaqueToOfficers",
                column: "PlaqueToContractorId",
                principalTable: "AkhPlaqueToContractors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AkhPlaqueToOfficers_AkhPlaqueToContractors_PlaqueToContractorId",
                table: "AkhPlaqueToOfficers");

            migrationBuilder.DropTable(
                name: "AkhPlaqueToContractors");

            migrationBuilder.DropIndex(
                name: "IX_AkhPlaqueToOfficers_PlaqueToContractorId",
                table: "AkhPlaqueToOfficers");

            migrationBuilder.DropColumn(
                name: "PlaqueToContractorId",
                table: "AkhPlaqueToOfficers");

            migrationBuilder.AddColumn<int>(
                name: "PlaqueToCityId",
                table: "AkhPlaqueToOfficers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AkhPlaqueToCities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CityInfoId = table.Column<int>(type: "int", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FromCode = table.Column<long>(type: "bigint", maxLength: 15, nullable: false),
                    LastCode = table.Column<long>(type: "bigint", maxLength: 15, nullable: false),
                    PlaqueToStateId = table.Column<int>(type: "int", nullable: true),
                    SetTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TenantId = table.Column<int>(type: "int", nullable: true),
                    ToCode = table.Column<long>(type: "bigint", maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AkhPlaqueToCities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AkhPlaqueToCities_AkhCityInfo_CityInfoId",
                        column: x => x.CityInfoId,
                        principalTable: "AkhCityInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AkhPlaqueToCities_AkhPlaqueToStates_PlaqueToStateId",
                        column: x => x.PlaqueToStateId,
                        principalTable: "AkhPlaqueToStates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AkhPlaqueToOfficers_PlaqueToCityId",
                table: "AkhPlaqueToOfficers",
                column: "PlaqueToCityId");

            migrationBuilder.CreateIndex(
                name: "IX_AkhPlaqueToCities_CityInfoId",
                table: "AkhPlaqueToCities",
                column: "CityInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_AkhPlaqueToCities_FromCode",
                table: "AkhPlaqueToCities",
                column: "FromCode");

            migrationBuilder.CreateIndex(
                name: "IX_AkhPlaqueToCities_PlaqueToStateId",
                table: "AkhPlaqueToCities",
                column: "PlaqueToStateId");

            migrationBuilder.CreateIndex(
                name: "IX_AkhPlaqueToCities_ToCode",
                table: "AkhPlaqueToCities",
                column: "ToCode");

            migrationBuilder.AddForeignKey(
                name: "FK_AkhPlaqueToOfficers_AkhPlaqueToCities_PlaqueToCityId",
                table: "AkhPlaqueToOfficers",
                column: "PlaqueToCityId",
                principalTable: "AkhPlaqueToCities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
