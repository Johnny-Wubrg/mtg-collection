using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagicCollection.Data.Migrations
{
    /// <inheritdoc />
    public partial class MultiplePrices : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_prints_cards_card_id",
                table: "prints");

            migrationBuilder.DropForeignKey(
                name: "fk_prints_editions_edition_id",
                table: "prints");

            migrationBuilder.DropColumn(
                name: "usd",
                table: "prints");

            migrationBuilder.AlterColumn<Guid>(
                name: "edition_id",
                table: "prints",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "card_id",
                table: "prints",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "price",
                columns: table => new
                {
                    printid = table.Column<Guid>(name: "print_id", type: "uuid", nullable: false),
                    treatmentid = table.Column<string>(name: "treatment_id", type: "character varying(16)", nullable: false),
                    amount = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_price", x => new { x.printid, x.treatmentid });
                    table.ForeignKey(
                        name: "fk_price_prints_print_id",
                        column: x => x.printid,
                        principalTable: "prints",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_price_treatments_treatment_id",
                        column: x => x.treatmentid,
                        principalTable: "treatments",
                        principalColumn: "identifier",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_price_treatment_id",
                table: "price",
                column: "treatment_id");

            migrationBuilder.AddForeignKey(
                name: "fk_prints_cards_card_id",
                table: "prints",
                column: "card_id",
                principalTable: "cards",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_prints_editions_edition_id",
                table: "prints",
                column: "edition_id",
                principalTable: "editions",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_prints_cards_card_id",
                table: "prints");

            migrationBuilder.DropForeignKey(
                name: "fk_prints_editions_edition_id",
                table: "prints");

            migrationBuilder.DropTable(
                name: "price");

            migrationBuilder.AlterColumn<Guid>(
                name: "edition_id",
                table: "prints",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "card_id",
                table: "prints",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<decimal>(
                name: "usd",
                table: "prints",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddForeignKey(
                name: "fk_prints_cards_card_id",
                table: "prints",
                column: "card_id",
                principalTable: "cards",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_prints_editions_edition_id",
                table: "prints",
                column: "edition_id",
                principalTable: "editions",
                principalColumn: "id");
        }
    }
}
