using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Akh.Breed.Migrations
{
    public partial class plaquenamespace : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AkhOfficers_AkhAcademicDegree_AcademicDegreeId",
                table: "AkhOfficers");

            migrationBuilder.DropForeignKey(
                name: "FK_AkhOfficers_AkhContractors_ContractorId",
                table: "AkhOfficers");

            migrationBuilder.DropForeignKey(
                name: "FK_AkhOfficers_AkhStateInfo_StateInfoId",
                table: "AkhOfficers");

            migrationBuilder.AlterColumn<int>(
                name: "StateInfoId",
                table: "AkhOfficers",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ContractorId",
                table: "AkhOfficers",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "AcademicDegreeId",
                table: "AkhOfficers",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<long>(
                name: "UserId",
                table: "AkhOfficers",
                nullable: true);

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
                    OfficerId = table.Column<int>(nullable: true),
                    StateId = table.Column<int>(nullable: true),
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
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AkhPlaqueInfos_AkhPlaqueState_StateId",
                        column: x => x.StateId,
                        principalTable: "AkhPlaqueState",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AkhPlaqueStores",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FromCode = table.Column<string>(maxLength: 15, nullable: false),
                    ToCode = table.Column<string>(maxLength: 15, nullable: false),
                    SetTime = table.Column<DateTime>(nullable: false),
                    SpeciesId = table.Column<int>(nullable: true),
                    FinishedPlaqueId = table.Column<long>(nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    TenantId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AkhPlaqueStores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AkhPlaqueStores_AkhPlaqueInfos_FinishedPlaqueId",
                        column: x => x.FinishedPlaqueId,
                        principalTable: "AkhPlaqueInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AkhPlaqueStores_AkhSpeciesInfo_SpeciesId",
                        column: x => x.SpeciesId,
                        principalTable: "AkhSpeciesInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PlaqueChanges",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PrePlaqueId = table.Column<long>(nullable: true),
                    NewPlaqueId = table.Column<long>(nullable: true),
                    ChangeReson = table.Column<string>(nullable: false),
                    SetTime = table.Column<DateTime>(nullable: false),
                    StateId = table.Column<int>(nullable: true),
                    OfficerId = table.Column<int>(nullable: true),
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
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PlaqueChanges_AkhOfficers_OfficerId",
                        column: x => x.OfficerId,
                        principalTable: "AkhOfficers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PlaqueChanges_AkhPlaqueInfos_PrePlaqueId",
                        column: x => x.PrePlaqueId,
                        principalTable: "AkhPlaqueInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PlaqueChanges_AkhPlaqueState_StateId",
                        column: x => x.StateId,
                        principalTable: "AkhPlaqueState",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AkhPlaqueOfficers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FromCode = table.Column<string>(maxLength: 15, nullable: false),
                    ToCode = table.Column<string>(maxLength: 15, nullable: false),
                    SetTime = table.Column<DateTime>(nullable: false),
                    OfficerId = table.Column<int>(nullable: true),
                    FinishedPlaqueId = table.Column<long>(nullable: true),
                    PlaqueStoreId = table.Column<int>(nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    TenantId = table.Column<int>(nullable: true)
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
                name: "IX_AkhPlaqueOfficers_FinishedPlaqueId",
                table: "AkhPlaqueOfficers",
                column: "FinishedPlaqueId");

            migrationBuilder.CreateIndex(
                name: "IX_AkhPlaqueOfficers_OfficerId",
                table: "AkhPlaqueOfficers",
                column: "OfficerId");

            migrationBuilder.CreateIndex(
                name: "IX_AkhPlaqueOfficers_PlaqueStoreId",
                table: "AkhPlaqueOfficers",
                column: "PlaqueStoreId");

            migrationBuilder.CreateIndex(
                name: "IX_AkhPlaqueStores_FinishedPlaqueId",
                table: "AkhPlaqueStores",
                column: "FinishedPlaqueId");

            migrationBuilder.CreateIndex(
                name: "IX_AkhPlaqueStores_SpeciesId",
                table: "AkhPlaqueStores",
                column: "SpeciesId");

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
                name: "FK_AkhOfficers_AkhAcademicDegree_AcademicDegreeId",
                table: "AkhOfficers",
                column: "AcademicDegreeId",
                principalTable: "AkhAcademicDegree",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AkhOfficers_AkhContractors_ContractorId",
                table: "AkhOfficers",
                column: "ContractorId",
                principalTable: "AkhContractors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AkhOfficers_AkhStateInfo_StateInfoId",
                table: "AkhOfficers",
                column: "StateInfoId",
                principalTable: "AkhStateInfo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AkhOfficers_AbpUsers_UserId",
                table: "AkhOfficers",
                column: "UserId",
                principalTable: "AbpUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AkhOfficers_AkhAcademicDegree_AcademicDegreeId",
                table: "AkhOfficers");

            migrationBuilder.DropForeignKey(
                name: "FK_AkhOfficers_AkhContractors_ContractorId",
                table: "AkhOfficers");

            migrationBuilder.DropForeignKey(
                name: "FK_AkhOfficers_AkhStateInfo_StateInfoId",
                table: "AkhOfficers");

            migrationBuilder.DropForeignKey(
                name: "FK_AkhOfficers_AbpUsers_UserId",
                table: "AkhOfficers");

            migrationBuilder.DropTable(
                name: "AkhPlaqueOfficers");

            migrationBuilder.DropTable(
                name: "PlaqueChanges");

            migrationBuilder.DropTable(
                name: "AkhPlaqueStores");

            migrationBuilder.DropTable(
                name: "AkhPlaqueInfos");

            migrationBuilder.DropIndex(
                name: "IX_AkhOfficers_UserId",
                table: "AkhOfficers");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "AkhOfficers");

            migrationBuilder.AlterColumn<int>(
                name: "StateInfoId",
                table: "AkhOfficers",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ContractorId",
                table: "AkhOfficers",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AcademicDegreeId",
                table: "AkhOfficers",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AkhOfficers_AkhAcademicDegree_AcademicDegreeId",
                table: "AkhOfficers",
                column: "AcademicDegreeId",
                principalTable: "AkhAcademicDegree",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AkhOfficers_AkhContractors_ContractorId",
                table: "AkhOfficers",
                column: "ContractorId",
                principalTable: "AkhContractors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AkhOfficers_AkhStateInfo_StateInfoId",
                table: "AkhOfficers",
                column: "StateInfoId",
                principalTable: "AkhStateInfo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
