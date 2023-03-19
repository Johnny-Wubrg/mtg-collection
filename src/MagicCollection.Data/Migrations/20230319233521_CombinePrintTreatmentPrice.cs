using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagicCollection.Data.Migrations
{
  /// <inheritdoc />
  public partial class CombinePrintTreatmentPrice : Migration
  {
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.AlterColumn<decimal>(
          name: "amount",
          table: "price",
          type: "numeric",
          nullable: true,
          oldClrType: typeof(decimal),
          oldType: "numeric");

      migrationBuilder.RenameColumn(
          name: "amount",
          table: "price",
          newName: "usd");

      migrationBuilder.Sql(@"
        insert into price (print_id, treatment_id, usd) 
        select 
            prints_id as print_id,
            available_treatments_identifier as treatment_id,
            null as usd
        from print_treatment t
        where not exists (
            select * 
            from price p 
            where p.print_id = t.prints_id 
            and p.treatment_id = t.available_treatments_identifier
        )");

      migrationBuilder.DropForeignKey(
          name: "fk_price_prints_print_id",
          table: "price");

      migrationBuilder.DropForeignKey(
          name: "fk_price_treatments_treatment_id",
          table: "price");

      migrationBuilder.DropTable(
          name: "print_treatment");

      migrationBuilder.DropPrimaryKey(
          name: "pk_price",
          table: "price");

      migrationBuilder.RenameTable(
          name: "price",
          newName: "print_treatment");

      migrationBuilder.RenameIndex(
          name: "ix_price_treatment_id",
          table: "print_treatment",
          newName: "ix_print_treatment_treatment_id");

      migrationBuilder.AlterColumn<decimal>(
          name: "usd",
          table: "print_treatment",
          type: "numeric",
          nullable: true,
          oldClrType: typeof(decimal),
          oldType: "numeric");

      migrationBuilder.AddPrimaryKey(
          name: "pk_print_treatment",
          table: "print_treatment",
          columns: new[] { "print_id", "treatment_id" });

      migrationBuilder.AddForeignKey(
          name: "fk_print_treatment_prints_print_id",
          table: "print_treatment",
          column: "print_id",
          principalTable: "prints",
          principalColumn: "id",
          onDelete: ReferentialAction.Cascade);

      migrationBuilder.AddForeignKey(
          name: "fk_print_treatment_treatments_treatment_id",
          table: "print_treatment",
          column: "treatment_id",
          principalTable: "treatments",
          principalColumn: "identifier",
          onDelete: ReferentialAction.Cascade);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropForeignKey(
          name: "fk_print_treatment_prints_print_id",
          table: "print_treatment");

      migrationBuilder.DropForeignKey(
          name: "fk_print_treatment_treatments_treatment_id",
          table: "print_treatment");

      migrationBuilder.DropPrimaryKey(
          name: "pk_print_treatment",
          table: "print_treatment");

      migrationBuilder.RenameTable(
          name: "print_treatment",
          newName: "price");

      migrationBuilder.RenameIndex(
          name: "ix_print_treatment_treatment_id",
          table: "price",
          newName: "ix_price_treatment_id");

      migrationBuilder.AddPrimaryKey(
          name: "pk_price",
          table: "price",
          columns: new[] { "print_id", "treatment_id" });

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
          name: "ix_print_treatment_prints_id",
          table: "print_treatment",
          column: "prints_id");

      migrationBuilder.AddForeignKey(
          name: "fk_price_prints_print_id",
          table: "price",
          column: "print_id",
          principalTable: "prints",
          principalColumn: "id",
          onDelete: ReferentialAction.Cascade);

      migrationBuilder.AddForeignKey(
          name: "fk_price_treatments_treatment_id",
          table: "price",
          column: "treatment_id",
          principalTable: "treatments",
          principalColumn: "identifier",
          onDelete: ReferentialAction.Cascade);

      migrationBuilder.Sql(@"
        insert into print_treatment (available_treatments_identifier, prints_id) 
        select 
            treatment_id as available_treatments_identifier,
            print_id as prints_id
        from price
        ");

      migrationBuilder.Sql("delete from price where usd is null");

      migrationBuilder.RenameColumn(
          name: "usd",
          table: "price",
          newName: "amount");

      migrationBuilder.AlterColumn<decimal>(
          name: "amount",
          table: "price",
          type: "numeric",
          nullable: false,
          defaultValue: 0m,
          oldClrType: typeof(decimal),
          oldType: "numeric",
          oldNullable: true);
    }
  }
}
