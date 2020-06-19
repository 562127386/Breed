using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Akh.Breed.Migrations
{
    public partial class addInseminationtable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "InseminationId",
                table: "AkhPlaqueInfos",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AkhInseminating",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NationalCode = table.Column<string>(nullable: false),
                    Latitude = table.Column<string>(nullable: true),
                    Longitude = table.Column<string>(nullable: true),
                    SpeciesInfoId = table.Column<int>(nullable: true),
                    BreedInfoId = table.Column<int>(nullable: true),
                    SexInfoId = table.Column<int>(nullable: true),
                    BirthDate = table.Column<DateTime>(nullable: false),
                    HerdId = table.Column<int>(nullable: true),
                    ActivityInfoId = table.Column<int>(nullable: true),
                    OfficerId = table.Column<int>(nullable: true),
                    LivestockFatherId = table.Column<int>(nullable: true),
                    NationalCodeFather = table.Column<string>(nullable: true),
                    BreedInfoFatherId = table.Column<int>(nullable: true),
                    LivestockMotherId = table.Column<int>(nullable: true),
                    NationalCodeMother = table.Column<string>(nullable: true),
                    BreedInfoMotherId = table.Column<int>(nullable: true),
                    EarNumber = table.Column<string>(nullable: true),
                    BodyNumber = table.Column<string>(nullable: true),
                    ForeignRegistrationNumber = table.Column<string>(nullable: true),
                    BirthTypeInfoId = table.Column<int>(nullable: true),
                    AnomalyInfoId = table.Column<int>(nullable: true),
                    MembershipInfoId = table.Column<int>(nullable: true),
                    IdIssueDate = table.Column<DateTime>(nullable: true),
                    BloodShare = table.Column<string>(nullable: true),
                    BreedShare = table.Column<string>(nullable: true),
                    BodyColorInfoId = table.Column<int>(nullable: true),
                    SpotColorInfoId = table.Column<int>(nullable: true),
                    SpotConnectorInfoId = table.Column<int>(nullable: true),
                    BreedName = table.Column<string>(nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    TenantId = table.Column<int>(nullable: true),
                    CreatorUserId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AkhInseminating", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AkhInseminating_AkhActivityInfo_ActivityInfoId",
                        column: x => x.ActivityInfoId,
                        principalTable: "AkhActivityInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AkhInseminating_AkhAnomalyInfo_AnomalyInfoId",
                        column: x => x.AnomalyInfoId,
                        principalTable: "AkhAnomalyInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AkhInseminating_AkhBirthTypeInfo_BirthTypeInfoId",
                        column: x => x.BirthTypeInfoId,
                        principalTable: "AkhBirthTypeInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AkhInseminating_AkhBodyColorInfo_BodyColorInfoId",
                        column: x => x.BodyColorInfoId,
                        principalTable: "AkhBodyColorInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AkhInseminating_AkhBreedInfo_BreedInfoFatherId",
                        column: x => x.BreedInfoFatherId,
                        principalTable: "AkhBreedInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AkhInseminating_AkhBreedInfo_BreedInfoId",
                        column: x => x.BreedInfoId,
                        principalTable: "AkhBreedInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AkhInseminating_AkhBreedInfo_BreedInfoMotherId",
                        column: x => x.BreedInfoMotherId,
                        principalTable: "AkhBreedInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AkhInseminating_AkhHerds_HerdId",
                        column: x => x.HerdId,
                        principalTable: "AkhHerds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AkhInseminating_AkhLivestocks_LivestockFatherId",
                        column: x => x.LivestockFatherId,
                        principalTable: "AkhLivestocks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AkhInseminating_AkhLivestocks_LivestockMotherId",
                        column: x => x.LivestockMotherId,
                        principalTable: "AkhLivestocks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AkhInseminating_AkhMembershipInfo_MembershipInfoId",
                        column: x => x.MembershipInfoId,
                        principalTable: "AkhMembershipInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AkhInseminating_AkhOfficers_OfficerId",
                        column: x => x.OfficerId,
                        principalTable: "AkhOfficers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AkhInseminating_AkhSexInfo_SexInfoId",
                        column: x => x.SexInfoId,
                        principalTable: "AkhSexInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AkhInseminating_AkhSpeciesInfo_SpeciesInfoId",
                        column: x => x.SpeciesInfoId,
                        principalTable: "AkhSpeciesInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AkhInseminating_AkhBodyColorInfo_SpotColorInfoId",
                        column: x => x.SpotColorInfoId,
                        principalTable: "AkhBodyColorInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AkhInseminating_AkhSpotConnectorInfo_SpotConnectorInfoId",
                        column: x => x.SpotConnectorInfoId,
                        principalTable: "AkhSpotConnectorInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AkhPlaqueInfos_InseminationId",
                table: "AkhPlaqueInfos",
                column: "InseminationId");

            migrationBuilder.CreateIndex(
                name: "IX_AkhInseminating_ActivityInfoId",
                table: "AkhInseminating",
                column: "ActivityInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_AkhInseminating_AnomalyInfoId",
                table: "AkhInseminating",
                column: "AnomalyInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_AkhInseminating_BirthTypeInfoId",
                table: "AkhInseminating",
                column: "BirthTypeInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_AkhInseminating_BodyColorInfoId",
                table: "AkhInseminating",
                column: "BodyColorInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_AkhInseminating_BreedInfoFatherId",
                table: "AkhInseminating",
                column: "BreedInfoFatherId");

            migrationBuilder.CreateIndex(
                name: "IX_AkhInseminating_BreedInfoId",
                table: "AkhInseminating",
                column: "BreedInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_AkhInseminating_BreedInfoMotherId",
                table: "AkhInseminating",
                column: "BreedInfoMotherId");

            migrationBuilder.CreateIndex(
                name: "IX_AkhInseminating_HerdId",
                table: "AkhInseminating",
                column: "HerdId");

            migrationBuilder.CreateIndex(
                name: "IX_AkhInseminating_LivestockFatherId",
                table: "AkhInseminating",
                column: "LivestockFatherId");

            migrationBuilder.CreateIndex(
                name: "IX_AkhInseminating_LivestockMotherId",
                table: "AkhInseminating",
                column: "LivestockMotherId");

            migrationBuilder.CreateIndex(
                name: "IX_AkhInseminating_MembershipInfoId",
                table: "AkhInseminating",
                column: "MembershipInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_AkhInseminating_OfficerId",
                table: "AkhInseminating",
                column: "OfficerId");

            migrationBuilder.CreateIndex(
                name: "IX_AkhInseminating_SexInfoId",
                table: "AkhInseminating",
                column: "SexInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_AkhInseminating_SpeciesInfoId",
                table: "AkhInseminating",
                column: "SpeciesInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_AkhInseminating_SpotColorInfoId",
                table: "AkhInseminating",
                column: "SpotColorInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_AkhInseminating_SpotConnectorInfoId",
                table: "AkhInseminating",
                column: "SpotConnectorInfoId");

            migrationBuilder.AddForeignKey(
                name: "FK_AkhPlaqueInfos_AkhInseminating_InseminationId",
                table: "AkhPlaqueInfos",
                column: "InseminationId",
                principalTable: "AkhInseminating",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AkhPlaqueInfos_AkhInseminating_InseminationId",
                table: "AkhPlaqueInfos");

            migrationBuilder.DropTable(
                name: "AkhInseminating");

            migrationBuilder.DropIndex(
                name: "IX_AkhPlaqueInfos_InseminationId",
                table: "AkhPlaqueInfos");

            migrationBuilder.DropColumn(
                name: "InseminationId",
                table: "AkhPlaqueInfos");
        }
    }
}
