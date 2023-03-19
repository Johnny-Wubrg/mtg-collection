using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagicCollection.Data.Migrations
{
    /// <inheritdoc />
    public partial class DefaultLanguage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_prints_languages_language_identifier",
                table: "prints");

            migrationBuilder.RenameColumn(
                name: "language_identifier",
                table: "prints",
                newName: "default_language_identifier");

            migrationBuilder.RenameIndex(
                name: "ix_prints_language_identifier",
                table: "prints",
                newName: "ix_prints_default_language_identifier");

            migrationBuilder.AddColumn<string>(
                name: "language_identifier",
                table: "card_entries",
                type: "character varying(4)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "ix_card_entries_language_identifier",
                table: "card_entries",
                column: "language_identifier");

            migrationBuilder.AddForeignKey(
                name: "fk_card_entries_languages_language_temp_id",
                table: "card_entries",
                column: "language_identifier",
                principalTable: "languages",
                principalColumn: "identifier");

            migrationBuilder.AddForeignKey(
                name: "fk_prints_languages_default_language_identifier",
                table: "prints",
                column: "default_language_identifier",
                principalTable: "languages",
                principalColumn: "identifier");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_card_entries_languages_language_temp_id",
                table: "card_entries");

            migrationBuilder.DropForeignKey(
                name: "fk_prints_languages_default_language_identifier",
                table: "prints");

            migrationBuilder.DropIndex(
                name: "ix_card_entries_language_identifier",
                table: "card_entries");

            migrationBuilder.DropColumn(
                name: "language_identifier",
                table: "card_entries");

            migrationBuilder.RenameColumn(
                name: "default_language_identifier",
                table: "prints",
                newName: "language_identifier");

            migrationBuilder.RenameIndex(
                name: "ix_prints_default_language_identifier",
                table: "prints",
                newName: "ix_prints_language_identifier");

            migrationBuilder.AddForeignKey(
                name: "fk_prints_languages_language_identifier",
                table: "prints",
                column: "language_identifier",
                principalTable: "languages",
                principalColumn: "identifier");
        }
    }
}
