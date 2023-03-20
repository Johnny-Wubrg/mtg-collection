using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagicCollection.Data.Migrations
{
    /// <inheritdoc />
    public partial class IncreaseCNLength : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_card_entries_languages_language_temp_id",
                table: "card_entries");

            migrationBuilder.DropForeignKey(
                name: "fk_card_entries_treatments_treatment_temp_id",
                table: "card_entries");

            migrationBuilder.AlterColumn<string>(
                name: "collector_number",
                table: "prints",
                type: "character varying(16)",
                maxLength: 16,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(8)",
                oldMaxLength: 8,
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "fk_card_entries_languages_language_identifier",
                table: "card_entries",
                column: "language_identifier",
                principalTable: "languages",
                principalColumn: "identifier");

            migrationBuilder.AddForeignKey(
                name: "fk_card_entries_treatments_treatment_identifier",
                table: "card_entries",
                column: "treatment_identifier",
                principalTable: "treatments",
                principalColumn: "identifier");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_card_entries_languages_language_identifier",
                table: "card_entries");

            migrationBuilder.DropForeignKey(
                name: "fk_card_entries_treatments_treatment_identifier",
                table: "card_entries");

            migrationBuilder.AlterColumn<string>(
                name: "collector_number",
                table: "prints",
                type: "character varying(8)",
                maxLength: 8,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(16)",
                oldMaxLength: 16,
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "fk_card_entries_languages_language_temp_id",
                table: "card_entries",
                column: "language_identifier",
                principalTable: "languages",
                principalColumn: "identifier");

            migrationBuilder.AddForeignKey(
                name: "fk_card_entries_treatments_treatment_temp_id",
                table: "card_entries",
                column: "treatment_identifier",
                principalTable: "treatments",
                principalColumn: "identifier");
        }
    }
}
