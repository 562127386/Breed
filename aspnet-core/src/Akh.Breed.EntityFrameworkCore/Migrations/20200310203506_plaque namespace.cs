using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Akh.Breed.Migrations
{
    public partial class plaquenamespace : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "UserId",
                table: "AkhOfficers",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "AkhPlaqueInfos",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(maxLength: 15, nullable: false),
                    SetTime = table.Column<DateTime>(nullable: false),
                    Longitude = table.Column<string>(nullable: false),
                    Latitude = table.Column<string>(nullable: false),
                    OfficerId = table.Column<int>(nullable: false),
                    StateId = table.Column<int>(nullable: false),
                    TenantId = table.Column<int>(nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AkhPlaqueInfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AkhPlaqueInfos_AkhOfficers_OfficerId",
                        column: x => x.OfficerId,
                        principalTable: "AkhOfficers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AkhPlaqueInfos_AkhPlaqueState_StateId",
                        column: x => x.StateId,
                        principalTable: "AkhPlaqueState",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlaqueChanges",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PrePlaqueId = table.Column<long>(nullable: false),
                    NewPlaqueId = table.Column<long>(nullable: false),
                    ChangeReson = table.Column<string>(nullable: false),
                    SetTime = table.Column<DateTime>(nullable: false),
                    StateId = table.Column<int>(nullable: false),
                    OfficerId = table.Column<int>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    TenantId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlaqueChanges", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlaqueChanges_AkhPlaqueInfos_NewPlaqueId",
                        column: x => x.NewPlaqueId,
                        principalTable: "AkhPlaqueInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlaqueChanges_AkhOfficers_OfficerId",
                        column: x => x.OfficerId,
                        principalTable: "AkhOfficers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlaqueChanges_AkhPlaqueInfos_PrePlaqueId",
                        column: x => x.PrePlaqueId,
                        principalTable: "AkhPlaqueInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlaqueChanges_AkhPlaqueState_StateId",
                        column: x => x.StateId,
                        principalTable: "AkhPlaqueState",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AkhOfficers_UserId",
                table: "AkhOfficers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AkhPlaqueInfos_Code",
                table: "AkhPlaqueInfos",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AkhPlaqueInfos_OfficerId",
                table: "AkhPlaqueInfos",
                column: "OfficerId");

            migrationBuilder.CreateIndex(
                name: "IX_AkhPlaqueInfos_StateId",
                table: "AkhPlaqueInfos",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_PlaqueChanges_NewPlaqueId",
                table: "PlaqueChanges",
                column: "NewPlaqueId");

            migrationBuilder.CreateIndex(
                name: "IX_PlaqueChanges_OfficerId",
                table: "PlaqueChanges",
                column: "OfficerId");

            migrationBuilder.CreateIndex(
                name: "IX_PlaqueChanges_PrePlaqueId",
                table: "PlaqueChanges",
                column: "PrePlaqueId");

            migrationBuilder.CreateIndex(
                name: "IX_PlaqueChanges_StateId",
                table: "PlaqueChanges",
                column: "StateId");

            migrationBuilder.AddForeignKey(
                name: "FK_AkhOfficers_AbpUsers_UserId",
                table: "AkhOfficers",
                column: "UserId",
                principalTable: "AbpUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AkhOfficers_AbpUsers_UserId",
                table: "AkhOfficers");

            migrationBuilder.DropTable(
                name: "PlaqueChanges");

            migrationBuilder.DropTable(
                name: "AkhPlaqueInfos");

            migrationBuilder.DropIndex(
                name: "IX_AkhOfficers_UserId",
                table: "AkhOfficers");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "AkhOfficers");
        }
    }
}
