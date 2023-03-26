using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagicCollection.Data.Migrations
{
    /// <inheritdoc />
    public partial class SetType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "type_identifier",
                table: "editions",
                type: "character varying(24)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "edition_type",
                columns: table => new
                {
                    identifier = table.Column<string>(type: "character varying(24)", maxLength: 24, nullable: false),
                    label = table.Column<string>(type: "character varying(24)", maxLength: 24, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_edition_type", x => x.identifier);
                });

            migrationBuilder.CreateIndex(
                name: "ix_editions_type_identifier",
                table: "editions",
                column: "type_identifier");

            migrationBuilder.AddForeignKey(
                name: "fk_editions_edition_type_type_temp_id",
                table: "editions",
                column: "type_identifier",
                principalTable: "edition_type",
                principalColumn: "identifier");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_editions_edition_type_type_temp_id",
                table: "editions");

            migrationBuilder.DropTable(
                name: "edition_type");

            migrationBuilder.DropIndex(
                name: "ix_editions_type_identifier",
                table: "editions");

            migrationBuilder.DropColumn(
                name: "type_identifier",
                table: "editions");
        }
    }
}
