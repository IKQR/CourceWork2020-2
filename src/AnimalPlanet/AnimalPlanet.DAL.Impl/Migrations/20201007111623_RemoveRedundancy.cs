using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace AnimalPlanet.DAL.Impl.Migrations
{
    public partial class RemoveRedundancy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NaturalAreas_Descriptions_DescriptionId",
                table: "NaturalAreas");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "ReserveSpecies");

            migrationBuilder.DropTable(
                name: "TextDescriptions");

            migrationBuilder.DropTable(
                name: "Descriptions");

            migrationBuilder.DropIndex(
                name: "IX_NaturalAreas_DescriptionId",
                table: "NaturalAreas");

            migrationBuilder.DropColumn(
                name: "DescriptionId",
                table: "NaturalAreas");

            migrationBuilder.AlterDatabase()
                .OldAnnotation("Npgsql:Enum:ImageFormat", "PNG,JPEG");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Species",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "NaturalAreaSpecies",
                columns: table => new
                {
                    NaturalAreaId = table.Column<int>(nullable: false),
                    SpecieId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NaturalAreaSpecies", x => new { x.NaturalAreaId, x.SpecieId });
                    table.ForeignKey(
                        name: "FK_NaturalAreaSpecies_NaturalAreas_NaturalAreaId",
                        column: x => x.NaturalAreaId,
                        principalTable: "NaturalAreas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NaturalAreaSpecies_Species_SpecieId",
                        column: x => x.SpecieId,
                        principalTable: "Species",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReserveSpecies",
                columns: table => new
                {
                    ReserveId = table.Column<int>(nullable: false),
                    SpecieId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReserveSpecies", x => new { x.ReserveId, x.SpecieId });
                    table.ForeignKey(
                        name: "FK_ReserveSpecies_Reserves_ReserveId",
                        column: x => x.ReserveId,
                        principalTable: "Reserves",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReserveSpecies_Species_SpecieId",
                        column: x => x.SpecieId,
                        principalTable: "Species",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NaturalAreaSpecies_SpecieId",
                table: "NaturalAreaSpecies",
                column: "SpecieId");

            migrationBuilder.CreateIndex(
                name: "IX_ReserveSpecies_SpecieId",
                table: "ReserveSpecies",
                column: "SpecieId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NaturalAreaSpecies");

            migrationBuilder.DropTable(
                name: "ReserveSpecies");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Species");

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:ImageFormat", "PNG,JPEG");

            migrationBuilder.AddColumn<int>(
                name: "DescriptionId",
                table: "NaturalAreas",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Descriptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NaturalAreaId = table.Column<int>(type: "integer", nullable: false),
                    ShortDesc = table.Column<string>(type: "text", nullable: true),
                    SpecieId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Descriptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Descriptions_Species_SpecieId",
                        column: x => x.SpecieId,
                        principalTable: "Species",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReserveSpecies",
                columns: table => new
                {
                    ReserveId = table.Column<int>(type: "integer", nullable: false),
                    SpecieId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReserveAnimals", x => new { x.ReserveId, x.SpecieId });
                    table.ForeignKey(
                        name: "FK_ReserveAnimals_Reserves_ReserveId",
                        column: x => x.ReserveId,
                        principalTable: "Reserves",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReserveAnimals_Species_SpecieId",
                        column: x => x.SpecieId,
                        principalTable: "Species",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DescriptionId = table.Column<int>(type: "integer", nullable: false),
                    Format = table.Column<int>(type: "\"ImageFormat\"", nullable: false),
                    ImageBytes = table.Column<byte[]>(type: "bytea", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Images_Descriptions_DescriptionId",
                        column: x => x.DescriptionId,
                        principalTable: "Descriptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TextDescriptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DescriptionId = table.Column<int>(type: "integer", nullable: false),
                    Text = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TextDescriptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TextDescriptions_Descriptions_DescriptionId",
                        column: x => x.DescriptionId,
                        principalTable: "Descriptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NaturalAreas_DescriptionId",
                table: "NaturalAreas",
                column: "DescriptionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Descriptions_SpecieId",
                table: "Descriptions",
                column: "SpecieId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Images_DescriptionId",
                table: "Images",
                column: "DescriptionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ReserveAnimals_SpecieId",
                table: "ReserveSpecies",
                column: "SpecieId");

            migrationBuilder.CreateIndex(
                name: "IX_TextDescriptions_DescriptionId",
                table: "TextDescriptions",
                column: "DescriptionId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_NaturalAreas_Descriptions_DescriptionId",
                table: "NaturalAreas",
                column: "DescriptionId",
                principalTable: "Descriptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
