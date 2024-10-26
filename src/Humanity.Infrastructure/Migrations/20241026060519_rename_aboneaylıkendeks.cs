using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Humanity.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class rename_aboneaylıkendeks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MusteriAylikEndeks_Abone_AboneId",
                table: "MusteriAylikEndeks");

            migrationBuilder.DropForeignKey(
                name: "FK_MusteriSaatlikEndeks_Abone_AboneId",
                table: "MusteriSaatlikEndeks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MusteriSaatlikEndeks",
                table: "MusteriSaatlikEndeks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MusteriAylikEndeks",
                table: "MusteriAylikEndeks");

            migrationBuilder.RenameTable(
                name: "MusteriSaatlikEndeks",
                newName: "AboneSaatlikEndeks");

            migrationBuilder.RenameTable(
                name: "MusteriAylikEndeks",
                newName: "AboneAylikEndeks");

            migrationBuilder.RenameIndex(
                name: "IX_MusteriSaatlikEndeks_AboneId",
                table: "AboneSaatlikEndeks",
                newName: "IX_AboneSaatlikEndeks_AboneId");

            migrationBuilder.RenameIndex(
                name: "IX_MusteriAylikEndeks_AboneId",
                table: "AboneAylikEndeks",
                newName: "IX_AboneAylikEndeks_AboneId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AboneSaatlikEndeks",
                table: "AboneSaatlikEndeks",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AboneAylikEndeks",
                table: "AboneAylikEndeks",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AboneAylikEndeks_Abone_AboneId",
                table: "AboneAylikEndeks",
                column: "AboneId",
                principalTable: "Abone",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AboneSaatlikEndeks_Abone_AboneId",
                table: "AboneSaatlikEndeks",
                column: "AboneId",
                principalTable: "Abone",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AboneAylikEndeks_Abone_AboneId",
                table: "AboneAylikEndeks");

            migrationBuilder.DropForeignKey(
                name: "FK_AboneSaatlikEndeks_Abone_AboneId",
                table: "AboneSaatlikEndeks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AboneSaatlikEndeks",
                table: "AboneSaatlikEndeks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AboneAylikEndeks",
                table: "AboneAylikEndeks");

            migrationBuilder.RenameTable(
                name: "AboneSaatlikEndeks",
                newName: "MusteriSaatlikEndeks");

            migrationBuilder.RenameTable(
                name: "AboneAylikEndeks",
                newName: "MusteriAylikEndeks");

            migrationBuilder.RenameIndex(
                name: "IX_AboneSaatlikEndeks_AboneId",
                table: "MusteriSaatlikEndeks",
                newName: "IX_MusteriSaatlikEndeks_AboneId");

            migrationBuilder.RenameIndex(
                name: "IX_AboneAylikEndeks_AboneId",
                table: "MusteriAylikEndeks",
                newName: "IX_MusteriAylikEndeks_AboneId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MusteriSaatlikEndeks",
                table: "MusteriSaatlikEndeks",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MusteriAylikEndeks",
                table: "MusteriAylikEndeks",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MusteriAylikEndeks_Abone_AboneId",
                table: "MusteriAylikEndeks",
                column: "AboneId",
                principalTable: "Abone",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MusteriSaatlikEndeks_Abone_AboneId",
                table: "MusteriSaatlikEndeks",
                column: "AboneId",
                principalTable: "Abone",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
