using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Akh.Breed.Migrations
{
    public partial class addherd2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Institution",
                table: "AkhHerds",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AkhLivestocks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NationalCode = table.Column<string>(nullable: false),
                    Latitude = table.Column<string>(nullable: true),
                    Longitude = table.Column<string>(nullable: true),
                    Imported = table.Column<bool>(nullable: false),
                    BirthDate = table.Column<DateTime>(nullable: false),
                    SpeciesInfoId = table.Column<int>(nullable: true),
                    SexInfoId = table.Column<int>(nullable: true),
                    HerdId = table.Column<int>(nullable: true),
                    ActivityInfoId = table.Column<int>(nullable: true),
                    OfficerId = table.Column<int>(nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    TenantId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AkhLivestocks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AkhLivestocks_AkhActivityInfo_ActivityInfoId",
                        column: x => x.ActivityInfoId,
                        principalTable: "AkhActivityInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AkhLivestocks_AkhHerds_HerdId",
                        column: x => x.HerdId,
                        principalTable: "AkhHerds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AkhLivestocks_AkhOfficers_OfficerId",
                        column: x => x.OfficerId,
                        principalTable: "AkhOfficers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AkhLivestocks_AkhSexInfo_SexInfoId",
                        column: x => x.SexInfoId,
                        principalTable: "AkhSexInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AkhLivestocks_AkhSpeciesInfo_SpeciesInfoId",
                        column: x => x.SpeciesInfoId,
                        principalTable: "AkhSpeciesInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AkhLivestocks_ActivityInfoId",
                table: "AkhLivestocks",
                column: "ActivityInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_AkhLivestocks_HerdId",
                table: "AkhLivestocks",
                column: "HerdId");

            migrationBuilder.CreateIndex(
                name: "IX_AkhLivestocks_OfficerId",
                table: "AkhLivestocks",
                column: "OfficerId");

            migrationBuilder.CreateIndex(
                name: "IX_AkhLivestocks_SexInfoId",
                table: "AkhLivestocks",
                column: "SexInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_AkhLivestocks_SpeciesInfoId",
                table: "AkhLivestocks",
                column: "SpeciesInfoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AkhLivestocks");

            migrationBuilder.DropColumn(
                name: "Institution",
                table: "AkhHerds");
        }
    }
}
