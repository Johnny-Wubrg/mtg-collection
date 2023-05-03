using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagicCollection.Data.Migrations
{
    /// <inheritdoc />
    public partial class NullableSections : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_card_entries_sections_section_id",
                table: "card_entries");

            migrationBuilder.AlterColumn<Guid>(
                name: "section_id",
                table: "card_entries",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "fk_card_entries_sections_section_id",
                table: "card_entries",
                column: "section_id",
                principalTable: "sections",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_card_entries_sections_section_id",
                table: "card_entries");

            migrationBuilder.AlterColumn<Guid>(
                name: "section_id",
                table: "card_entries",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "fk_card_entries_sections_section_id",
                table: "card_entries",
                column: "section_id",
                principalTable: "sections",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
