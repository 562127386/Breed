using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Akh.Breed.Migrations
{
    public partial class allocateadd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AkhPlaqueOfficers");

            migrationBuilder.CreateTable(
                name: "AkhPlaqueToStates",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FromCode = table.Column<long>(maxLength: 15, nullable: false),
                    ToCode = table.Column<long>(maxLength: 15, nullable: false),
                    LastCode = table.Column<long>(maxLength: 15, nullable: false),
                    SetTime = table.Column<DateTime>(nullable: false),
                    StateInfoId = table.Column<int>(nullable: true),
                    PlaqueStoreId = table.Column<int>(nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    TenantId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AkhPlaqueToStates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AkhPlaqueToStates_AkhPlaqueStores_PlaqueStoreId",
                        column: x => x.PlaqueStoreId,
                        principalTable: "AkhPlaqueStores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AkhPlaqueToStates_AkhStateInfo_StateInfoId",
                        column: x => x.StateInfoId,
                        principalTable: "AkhStateInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AkhPlaqueToCities",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FromCode = table.Column<long>(maxLength: 15, nullable: false),
                    ToCode = table.Column<long>(maxLength: 15, nullable: false),
                    SetTime = table.Column<DateTime>(nullable: false),
                    CityInfoId = table.Column<int>(nullable: true),
                    PlaqueToStateId = table.Column<int>(nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    TenantId = table.Column<int>(nullable: true)
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

            migrationBuilder.CreateTable(
                name: "AkhPlaqueToOfficers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FromCode = table.Column<long>(maxLength: 15, nullable: false),
                    ToCode = table.Column<long>(maxLength: 15, nullable: false),
                    LastCode = table.Column<long>(maxLength: 15, nullable: false),
                    SetTime = table.Column<DateTime>(nullable: false),
                    OfficerId = table.Column<int>(nullable: true),
                    PlaqueToCityId = table.Column<int>(nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    TenantId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AkhPlaqueToOfficers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AkhPlaqueToOfficers_AkhOfficers_OfficerId",
                        column: x => x.OfficerId,
                        principalTable: "AkhOfficers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AkhPlaqueToOfficers_AkhPlaqueToCities_PlaqueToCityId",
                        column: x => x.PlaqueToCityId,
                        principalTable: "AkhPlaqueToCities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_AkhPlaqueToOfficers_FromCode",
                table: "AkhPlaqueToOfficers",
                column: "FromCode");

            migrationBuilder.CreateIndex(
                name: "IX_AkhPlaqueToOfficers_OfficerId",
                table: "AkhPlaqueToOfficers",
                column: "OfficerId");

            migrationBuilder.CreateIndex(
                name: "IX_AkhPlaqueToOfficers_PlaqueToCityId",
                table: "AkhPlaqueToOfficers",
                column: "PlaqueToCityId");

            migrationBuilder.CreateIndex(
                name: "IX_AkhPlaqueToOfficers_ToCode",
                table: "AkhPlaqueToOfficers",
                column: "ToCode");

            migrationBuilder.CreateIndex(
                name: "IX_AkhPlaqueToStates_FromCode",
                table: "AkhPlaqueToStates",
                column: "FromCode");

            migrationBuilder.CreateIndex(
                name: "IX_AkhPlaqueToStates_PlaqueStoreId",
                table: "AkhPlaqueToStates",
                column: "PlaqueStoreId");

            migrationBuilder.CreateIndex(
                name: "IX_AkhPlaqueToStates_StateInfoId",
                table: "AkhPlaqueToStates",
                column: "StateInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_AkhPlaqueToStates_ToCode",
                table: "AkhPlaqueToStates",
                column: "ToCode");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AkhPlaqueToOfficers");

            migrationBuilder.DropTable(
                name: "AkhPlaqueToCities");

            migrationBuilder.DropTable(
                name: "AkhPlaqueToStates");

            migrationBuilder.CreateTable(
                name: "AkhPlaqueOfficers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FinishedPlaqueId = table.Column<long>(type: "bigint", nullable: true),
                    FromCode = table.Column<long>(type: "bigint", maxLength: 15, nullable: false),
                    OfficerId = table.Column<int>(type: "int", nullable: true),
                    PlaqueStoreId = table.Column<int>(type: "int", nullable: true),
                    SetTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TenantId = table.Column<int>(type: "int", nullable: true),
                    ToCode = table.Column<long>(type: "bigint", maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AkhPlaqueOfficers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AkhPlaqueOfficers_AkhPlaqueInfos_FinishedPlaqueId",
                        column: x => x.FinishedPlaqueId,
                        principalTable: "AkhPlaqueInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AkhPlaqueOfficers_AkhOfficers_OfficerId",
                        column: x => x.OfficerId,
                        principalTable: "AkhOfficers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AkhPlaqueOfficers_AkhPlaqueStores_PlaqueStoreId",
                        column: x => x.PlaqueStoreId,
                        principalTable: "AkhPlaqueStores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AkhPlaqueOfficers_FinishedPlaqueId",
                table: "AkhPlaqueOfficers",
                column: "FinishedPlaqueId");

            migrationBuilder.CreateIndex(
                name: "IX_AkhPlaqueOfficers_FromCode",
                table: "AkhPlaqueOfficers",
                column: "FromCode");

            migrationBuilder.CreateIndex(
                name: "IX_AkhPlaqueOfficers_OfficerId",
                table: "AkhPlaqueOfficers",
                column: "OfficerId");

            migrationBuilder.CreateIndex(
                name: "IX_AkhPlaqueOfficers_PlaqueStoreId",
                table: "AkhPlaqueOfficers",
                column: "PlaqueStoreId");

            migrationBuilder.CreateIndex(
                name: "IX_AkhPlaqueOfficers_ToCode",
                table: "AkhPlaqueOfficers",
                column: "ToCode");
        }
    }
}
