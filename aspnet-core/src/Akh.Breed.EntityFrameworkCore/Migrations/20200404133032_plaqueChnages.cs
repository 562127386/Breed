using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Akh.Breed.Migrations
{
    public partial class plaqueChnages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlaqueChanges_AkhPlaqueInfos_NewPlaqueId",
                table: "PlaqueChanges");

            migrationBuilder.DropForeignKey(
                name: "FK_PlaqueChanges_AkhPlaqueInfos_PrePlaqueId",
                table: "PlaqueChanges");

            migrationBuilder.DropForeignKey(
                name: "FK_PlaqueChanges_AkhPlaqueState_StateId",
                table: "PlaqueChanges");

            migrationBuilder.DropIndex(
                name: "IX_PlaqueChanges_NewPlaqueId",
                table: "PlaqueChanges");

            migrationBuilder.DropIndex(
                name: "IX_PlaqueChanges_PrePlaqueId",
                table: "PlaqueChanges");

            migrationBuilder.DropIndex(
                name: "IX_PlaqueChanges_StateId",
                table: "PlaqueChanges");

            migrationBuilder.DropColumn(
                name: "ChangeReson",
                table: "PlaqueChanges");

            migrationBuilder.DropColumn(
                name: "NewPlaqueId",
                table: "PlaqueChanges");

            migrationBuilder.DropColumn(
                name: "PrePlaqueId",
                table: "PlaqueChanges");

            migrationBuilder.DropColumn(
                name: "SetTime",
                table: "PlaqueChanges");

            migrationBuilder.DropColumn(
                name: "StateId",
                table: "PlaqueChanges");

            migrationBuilder.AddColumn<string>(
                name: "ChangeReason",
                table: "PlaqueChanges",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NewStateId",
                table: "PlaqueChanges",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "PlaqueId",
                table: "PlaqueChanges",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PreStateId",
                table: "PlaqueChanges",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PlaqueChanges_NewStateId",
                table: "PlaqueChanges",
                column: "NewStateId");

            migrationBuilder.CreateIndex(
                name: "IX_PlaqueChanges_PlaqueId",
                table: "PlaqueChanges",
                column: "PlaqueId");

            migrationBuilder.CreateIndex(
                name: "IX_PlaqueChanges_PreStateId",
                table: "PlaqueChanges",
                column: "PreStateId");

            migrationBuilder.AddForeignKey(
                name: "FK_PlaqueChanges_AkhPlaqueState_NewStateId",
                table: "PlaqueChanges",
                column: "NewStateId",
                principalTable: "AkhPlaqueState",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PlaqueChanges_AkhPlaqueInfos_PlaqueId",
                table: "PlaqueChanges",
                column: "PlaqueId",
                principalTable: "AkhPlaqueInfos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PlaqueChanges_AkhPlaqueState_PreStateId",
                table: "PlaqueChanges",
                column: "PreStateId",
                principalTable: "AkhPlaqueState",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlaqueChanges_AkhPlaqueState_NewStateId",
                table: "PlaqueChanges");

            migrationBuilder.DropForeignKey(
                name: "FK_PlaqueChanges_AkhPlaqueInfos_PlaqueId",
                table: "PlaqueChanges");

            migrationBuilder.DropForeignKey(
                name: "FK_PlaqueChanges_AkhPlaqueState_PreStateId",
                table: "PlaqueChanges");

            migrationBuilder.DropIndex(
                name: "IX_PlaqueChanges_NewStateId",
                table: "PlaqueChanges");

            migrationBuilder.DropIndex(
                name: "IX_PlaqueChanges_PlaqueId",
                table: "PlaqueChanges");

            migrationBuilder.DropIndex(
                name: "IX_PlaqueChanges_PreStateId",
                table: "PlaqueChanges");

            migrationBuilder.DropColumn(
                name: "ChangeReason",
                table: "PlaqueChanges");

            migrationBuilder.DropColumn(
                name: "NewStateId",
                table: "PlaqueChanges");

            migrationBuilder.DropColumn(
                name: "PlaqueId",
                table: "PlaqueChanges");

            migrationBuilder.DropColumn(
                name: "PreStateId",
                table: "PlaqueChanges");

            migrationBuilder.AddColumn<string>(
                name: "ChangeReson",
                table: "PlaqueChanges",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<long>(
                name: "NewPlaqueId",
                table: "PlaqueChanges",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "PrePlaqueId",
                table: "PlaqueChanges",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "SetTime",
                table: "PlaqueChanges",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "StateId",
                table: "PlaqueChanges",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PlaqueChanges_NewPlaqueId",
                table: "PlaqueChanges",
                column: "NewPlaqueId");

            migrationBuilder.CreateIndex(
                name: "IX_PlaqueChanges_PrePlaqueId",
                table: "PlaqueChanges",
                column: "PrePlaqueId");

            migrationBuilder.CreateIndex(
                name: "IX_PlaqueChanges_StateId",
                table: "PlaqueChanges",
                column: "StateId");

            migrationBuilder.AddForeignKey(
                name: "FK_PlaqueChanges_AkhPlaqueInfos_NewPlaqueId",
                table: "PlaqueChanges",
                column: "NewPlaqueId",
                principalTable: "AkhPlaqueInfos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PlaqueChanges_AkhPlaqueInfos_PrePlaqueId",
                table: "PlaqueChanges",
                column: "PrePlaqueId",
                principalTable: "AkhPlaqueInfos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PlaqueChanges_AkhPlaqueState_StateId",
                table: "PlaqueChanges",
                column: "StateId",
                principalTable: "AkhPlaqueState",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
