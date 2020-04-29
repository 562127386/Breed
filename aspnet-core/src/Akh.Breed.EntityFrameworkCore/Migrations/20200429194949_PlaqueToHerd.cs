using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Akh.Breed.Migrations
{
    public partial class PlaqueToHerd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AkhPlaqueToHerds",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NationalCode = table.Column<string>(nullable: false),
                    Latitude = table.Column<string>(nullable: true),
                    Longitude = table.Column<string>(nullable: true),
                    HerdId = table.Column<int>(nullable: true),
                    OfficerId = table.Column<int>(nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    TenantId = table.Column<int>(nullable: true),
                    CreatorUserId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AkhPlaqueToHerds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AkhPlaqueToHerds_AkhHerds_HerdId",
                        column: x => x.HerdId,
                        principalTable: "AkhHerds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AkhPlaqueToHerds_AkhOfficers_OfficerId",
                        column: x => x.OfficerId,
                        principalTable: "AkhOfficers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AkhPlaqueToHerds_HerdId",
                table: "AkhPlaqueToHerds",
                column: "HerdId");

            migrationBuilder.CreateIndex(
                name: "IX_AkhPlaqueToHerds_NationalCode",
                table: "AkhPlaqueToHerds",
                column: "NationalCode");

            migrationBuilder.CreateIndex(
                name: "IX_AkhPlaqueToHerds_OfficerId",
                table: "AkhPlaqueToHerds",
                column: "OfficerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AkhPlaqueToHerds");
        }
    }
}
