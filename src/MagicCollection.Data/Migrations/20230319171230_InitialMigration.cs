using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagicCollection.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:uuid-ossp", ",,");

            migrationBuilder.CreateTable(
                name: "bins",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    label = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_bins", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "cards",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cards", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "editions",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    code = table.Column<string>(type: "character varying(8)", maxLength: 8, nullable: true),
                    name = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    releasedate = table.Column<DateOnly>(name: "release_date", type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_editions", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "languages",
                columns: table => new
                {
                    identifier = table.Column<string>(type: "character varying(4)", maxLength: 4, nullable: false),
                    label = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_languages", x => x.identifier);
                });

            migrationBuilder.CreateTable(
                name: "rarities",
                columns: table => new
                {
                    identifier = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: false),
                    label = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_rarities", x => x.identifier);
                });

            migrationBuilder.CreateTable(
                name: "treatments",
                columns: table => new
                {
                    identifier = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: false),
                    label = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_treatments", x => x.identifier);
                });

            migrationBuilder.CreateTable(
                name: "sections",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    label = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    binid = table.Column<Guid>(name: "bin_id", type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_sections", x => x.id);
                    table.ForeignKey(
                        name: "fk_sections_bins_bin_id",
                        column: x => x.binid,
                        principalTable: "bins",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "prints",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    cardid = table.Column<Guid>(name: "card_id", type: "uuid", nullable: true),
                    editionid = table.Column<Guid>(name: "edition_id", type: "uuid", nullable: true),
                    collectornumber = table.Column<string>(name: "collector_number", type: "character varying(8)", maxLength: 8, nullable: true),
                    usd = table.Column<decimal>(type: "numeric", nullable: false),
                    rarityidentifier = table.Column<string>(name: "rarity_identifier", type: "character varying(16)", nullable: true),
                    languageidentifier = table.Column<string>(name: "language_identifier", type: "character varying(4)", nullable: true),
                    scryfalluri = table.Column<string>(name: "scryfall_uri", type: "character varying(256)", maxLength: 256, nullable: false),
                    scryfallimageuri = table.Column<string>(name: "scryfall_image_uri", type: "character varying(256)", maxLength: 256, nullable: true),
                    dateupdated = table.Column<DateTime>(name: "date_updated", type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_prints", x => x.id);
                    table.ForeignKey(
                        name: "fk_prints_cards_card_id",
                        column: x => x.cardid,
                        principalTable: "cards",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_prints_editions_edition_id",
                        column: x => x.editionid,
                        principalTable: "editions",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_prints_languages_language_identifier",
                        column: x => x.languageidentifier,
                        principalTable: "languages",
                        principalColumn: "identifier");
                    table.ForeignKey(
                        name: "fk_prints_rarities_rarity_temp_id",
                        column: x => x.rarityidentifier,
                        principalTable: "rarities",
                        principalColumn: "identifier");
                });

            migrationBuilder.CreateTable(
                name: "card_entries",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    quantity = table.Column<int>(type: "integer", nullable: false),
                    printid = table.Column<Guid>(name: "print_id", type: "uuid", nullable: true),
                    treatmentidentifier = table.Column<string>(name: "treatment_identifier", type: "character varying(16)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_card_entries", x => x.id);
                    table.ForeignKey(
                        name: "fk_card_entries_prints_print_id",
                        column: x => x.printid,
                        principalTable: "prints",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_card_entries_treatments_treatment_temp_id",
                        column: x => x.treatmentidentifier,
                        principalTable: "treatments",
                        principalColumn: "identifier");
                });

            migrationBuilder.CreateTable(
                name: "print_treatment",
                columns: table => new
                {
                    availabletreatmentsidentifier = table.Column<string>(name: "available_treatments_identifier", type: "character varying(16)", nullable: false),
                    printsid = table.Column<Guid>(name: "prints_id", type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_print_treatment", x => new { x.availabletreatmentsidentifier, x.printsid });
                    table.ForeignKey(
                        name: "fk_print_treatment_prints_prints_id",
                        column: x => x.printsid,
                        principalTable: "prints",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_print_treatment_treatments_available_treatments_identifier",
                        column: x => x.availabletreatmentsidentifier,
                        principalTable: "treatments",
                        principalColumn: "identifier",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_card_entries_print_id",
                table: "card_entries",
                column: "print_id");

            migrationBuilder.CreateIndex(
                name: "ix_card_entries_treatment_identifier",
                table: "card_entries",
                column: "treatment_identifier");

            migrationBuilder.CreateIndex(
                name: "ix_print_treatment_prints_id",
                table: "print_treatment",
                column: "prints_id");

            migrationBuilder.CreateIndex(
                name: "ix_prints_card_id",
                table: "prints",
                column: "card_id");

            migrationBuilder.CreateIndex(
                name: "ix_prints_edition_id",
                table: "prints",
                column: "edition_id");

            migrationBuilder.CreateIndex(
                name: "ix_prints_language_identifier",
                table: "prints",
                column: "language_identifier");

            migrationBuilder.CreateIndex(
                name: "ix_prints_rarity_identifier",
                table: "prints",
                column: "rarity_identifier");

            migrationBuilder.CreateIndex(
                name: "ix_sections_bin_id",
                table: "sections",
                column: "bin_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "card_entries");

            migrationBuilder.DropTable(
                name: "print_treatment");

            migrationBuilder.DropTable(
                name: "sections");

            migrationBuilder.DropTable(
                name: "prints");

            migrationBuilder.DropTable(
                name: "treatments");

            migrationBuilder.DropTable(
                name: "bins");

            migrationBuilder.DropTable(
                name: "cards");

            migrationBuilder.DropTable(
                name: "editions");

            migrationBuilder.DropTable(
                name: "languages");

            migrationBuilder.DropTable(
                name: "rarities");
        }
    }
}
