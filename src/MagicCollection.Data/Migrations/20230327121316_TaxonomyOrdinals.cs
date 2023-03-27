using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagicCollection.Data.Migrations
{
    /// <inheritdoc />
    public partial class TaxonomyOrdinals : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ordinal",
                table: "treatments",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ordinal",
                table: "languages",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ordinal",
                table: "edition_type",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ordinal",
                table: "treatments");

            migrationBuilder.DropColumn(
                name: "ordinal",
                table: "languages");

            migrationBuilder.DropColumn(
                name: "ordinal",
                table: "edition_type");
        }
    }
}
