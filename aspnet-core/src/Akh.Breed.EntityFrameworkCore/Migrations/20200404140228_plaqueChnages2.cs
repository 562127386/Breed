using Microsoft.EntityFrameworkCore.Migrations;

namespace Akh.Breed.Migrations
{
    public partial class plaqueChnages2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlaqueChanges_AkhPlaqueState_NewStateId",
                table: "PlaqueChanges");

            migrationBuilder.DropForeignKey(
                name: "FK_PlaqueChanges_AkhOfficers_OfficerId",
                table: "PlaqueChanges");

            migrationBuilder.DropForeignKey(
                name: "FK_PlaqueChanges_AkhPlaqueInfos_PlaqueId",
                table: "PlaqueChanges");

            migrationBuilder.DropForeignKey(
                name: "FK_PlaqueChanges_AkhPlaqueState_PreStateId",
                table: "PlaqueChanges");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PlaqueChanges",
                table: "PlaqueChanges");

            migrationBuilder.RenameTable(
                name: "PlaqueChanges",
                newName: "AkhPlaqueChanges");

            migrationBuilder.RenameIndex(
                name: "IX_PlaqueChanges_PreStateId",
                table: "AkhPlaqueChanges",
                newName: "IX_AkhPlaqueChanges_PreStateId");

            migrationBuilder.RenameIndex(
                name: "IX_PlaqueChanges_PlaqueId",
                table: "AkhPlaqueChanges",
                newName: "IX_AkhPlaqueChanges_PlaqueId");

            migrationBuilder.RenameIndex(
                name: "IX_PlaqueChanges_OfficerId",
                table: "AkhPlaqueChanges",
                newName: "IX_AkhPlaqueChanges_OfficerId");

            migrationBuilder.RenameIndex(
                name: "IX_PlaqueChanges_NewStateId",
                table: "AkhPlaqueChanges",
                newName: "IX_AkhPlaqueChanges_NewStateId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AkhPlaqueChanges",
                table: "AkhPlaqueChanges",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AkhPlaqueChanges_AkhPlaqueState_NewStateId",
                table: "AkhPlaqueChanges",
                column: "NewStateId",
                principalTable: "AkhPlaqueState",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AkhPlaqueChanges_AkhOfficers_OfficerId",
                table: "AkhPlaqueChanges",
                column: "OfficerId",
                principalTable: "AkhOfficers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AkhPlaqueChanges_AkhPlaqueInfos_PlaqueId",
                table: "AkhPlaqueChanges",
                column: "PlaqueId",
                principalTable: "AkhPlaqueInfos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AkhPlaqueChanges_AkhPlaqueState_PreStateId",
                table: "AkhPlaqueChanges",
                column: "PreStateId",
                principalTable: "AkhPlaqueState",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AkhPlaqueChanges_AkhPlaqueState_NewStateId",
                table: "AkhPlaqueChanges");

            migrationBuilder.DropForeignKey(
                name: "FK_AkhPlaqueChanges_AkhOfficers_OfficerId",
                table: "AkhPlaqueChanges");

            migrationBuilder.DropForeignKey(
                name: "FK_AkhPlaqueChanges_AkhPlaqueInfos_PlaqueId",
                table: "AkhPlaqueChanges");

            migrationBuilder.DropForeignKey(
                name: "FK_AkhPlaqueChanges_AkhPlaqueState_PreStateId",
                table: "AkhPlaqueChanges");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AkhPlaqueChanges",
                table: "AkhPlaqueChanges");

            migrationBuilder.RenameTable(
                name: "AkhPlaqueChanges",
                newName: "PlaqueChanges");

            migrationBuilder.RenameIndex(
                name: "IX_AkhPlaqueChanges_PreStateId",
                table: "PlaqueChanges",
                newName: "IX_PlaqueChanges_PreStateId");

            migrationBuilder.RenameIndex(
                name: "IX_AkhPlaqueChanges_PlaqueId",
                table: "PlaqueChanges",
                newName: "IX_PlaqueChanges_PlaqueId");

            migrationBuilder.RenameIndex(
                name: "IX_AkhPlaqueChanges_OfficerId",
                table: "PlaqueChanges",
                newName: "IX_PlaqueChanges_OfficerId");

            migrationBuilder.RenameIndex(
                name: "IX_AkhPlaqueChanges_NewStateId",
                table: "PlaqueChanges",
                newName: "IX_PlaqueChanges_NewStateId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PlaqueChanges",
                table: "PlaqueChanges",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PlaqueChanges_AkhPlaqueState_NewStateId",
                table: "PlaqueChanges",
                column: "NewStateId",
                principalTable: "AkhPlaqueState",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PlaqueChanges_AkhOfficers_OfficerId",
                table: "PlaqueChanges",
                column: "OfficerId",
                principalTable: "AkhOfficers",
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
    }
}
