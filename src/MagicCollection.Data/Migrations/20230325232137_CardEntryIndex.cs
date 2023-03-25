using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagicCollection.Data.Migrations
{
    /// <inheritdoc />
    public partial class CardEntryIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_card_entries_print_id",
                table: "card_entries");

            migrationBuilder.CreateIndex(
                name: "ix_card_entries_print_id_treatment_identifier_language_identif",
                table: "card_entries",
                columns: new[] { "print_id", "treatment_identifier", "language_identifier", "section_id" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_card_entries_print_id_treatment_identifier_language_identif",
                table: "card_entries");

            migrationBuilder.CreateIndex(
                name: "ix_card_entries_print_id",
                table: "card_entries",
                column: "print_id");
        }
    }
}
