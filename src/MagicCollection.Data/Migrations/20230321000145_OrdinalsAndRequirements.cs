using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagicCollection.Data.Migrations
{
    /// <inheritdoc />
    public partial class OrdinalsAndRequirements : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_card_entries_languages_language_identifier",
                table: "card_entries");

            migrationBuilder.DropForeignKey(
                name: "fk_card_entries_prints_print_id",
                table: "card_entries");

            migrationBuilder.DropForeignKey(
                name: "fk_card_entries_treatments_treatment_identifier",
                table: "card_entries");

            migrationBuilder.DropForeignKey(
                name: "fk_prints_languages_default_language_identifier",
                table: "prints");

            migrationBuilder.DropForeignKey(
                name: "fk_prints_rarities_rarity_temp_id",
                table: "prints");

            migrationBuilder.AddColumn<int>(
                name: "ordinal",
                table: "sections",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ordinal",
                table: "rarities",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "rarity_identifier",
                table: "prints",
                type: "character varying(16)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(16)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "default_language_identifier",
                table: "prints",
                type: "character varying(4)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(4)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "treatment_identifier",
                table: "card_entries",
                type: "character varying(16)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(16)",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "print_id",
                table: "card_entries",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "language_identifier",
                table: "card_entries",
                type: "character varying(4)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(4)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ordinal",
                table: "bins",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "fk_card_entries_languages_language_identifier",
                table: "card_entries",
                column: "language_identifier",
                principalTable: "languages",
                principalColumn: "identifier",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_card_entries_prints_print_id",
                table: "card_entries",
                column: "print_id",
                principalTable: "prints",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_card_entries_treatments_treatment_identifier",
                table: "card_entries",
                column: "treatment_identifier",
                principalTable: "treatments",
                principalColumn: "identifier",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_prints_languages_default_language_identifier",
                table: "prints",
                column: "default_language_identifier",
                principalTable: "languages",
                principalColumn: "identifier",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_prints_rarities_rarity_temp_id",
                table: "prints",
                column: "rarity_identifier",
                principalTable: "rarities",
                principalColumn: "identifier",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_card_entries_languages_language_identifier",
                table: "card_entries");

            migrationBuilder.DropForeignKey(
                name: "fk_card_entries_prints_print_id",
                table: "card_entries");

            migrationBuilder.DropForeignKey(
                name: "fk_card_entries_treatments_treatment_identifier",
                table: "card_entries");

            migrationBuilder.DropForeignKey(
                name: "fk_prints_languages_default_language_identifier",
                table: "prints");

            migrationBuilder.DropForeignKey(
                name: "fk_prints_rarities_rarity_temp_id",
                table: "prints");

            migrationBuilder.DropColumn(
                name: "ordinal",
                table: "sections");

            migrationBuilder.DropColumn(
                name: "ordinal",
                table: "rarities");

            migrationBuilder.DropColumn(
                name: "ordinal",
                table: "bins");

            migrationBuilder.AlterColumn<string>(
                name: "rarity_identifier",
                table: "prints",
                type: "character varying(16)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(16)");

            migrationBuilder.AlterColumn<string>(
                name: "default_language_identifier",
                table: "prints",
                type: "character varying(4)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(4)");

            migrationBuilder.AlterColumn<string>(
                name: "treatment_identifier",
                table: "card_entries",
                type: "character varying(16)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(16)");

            migrationBuilder.AlterColumn<Guid>(
                name: "print_id",
                table: "card_entries",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<string>(
                name: "language_identifier",
                table: "card_entries",
                type: "character varying(4)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(4)");

            migrationBuilder.AddForeignKey(
                name: "fk_card_entries_languages_language_identifier",
                table: "card_entries",
                column: "language_identifier",
                principalTable: "languages",
                principalColumn: "identifier");

            migrationBuilder.AddForeignKey(
                name: "fk_card_entries_prints_print_id",
                table: "card_entries",
                column: "print_id",
                principalTable: "prints",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_card_entries_treatments_treatment_identifier",
                table: "card_entries",
                column: "treatment_identifier",
                principalTable: "treatments",
                principalColumn: "identifier");

            migrationBuilder.AddForeignKey(
                name: "fk_prints_languages_default_language_identifier",
                table: "prints",
                column: "default_language_identifier",
                principalTable: "languages",
                principalColumn: "identifier");

            migrationBuilder.AddForeignKey(
                name: "fk_prints_rarities_rarity_temp_id",
                table: "prints",
                column: "rarity_identifier",
                principalTable: "rarities",
                principalColumn: "identifier");
        }
    }
}
