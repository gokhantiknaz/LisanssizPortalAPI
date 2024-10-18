using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Humanity.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class firma2musteri : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FirmaEntegrasyon_Musteri_MusteriId",
                table: "FirmaEntegrasyon");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FirmaEntegrasyon",
                table: "FirmaEntegrasyon");

            migrationBuilder.RenameTable(
                name: "FirmaEntegrasyon",
                newName: "MusteriEntegrasyon");

            migrationBuilder.RenameIndex(
                name: "IX_FirmaEntegrasyon_MusteriId",
                table: "MusteriEntegrasyon",
                newName: "IX_MusteriEntegrasyon_MusteriId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MusteriEntegrasyon",
                table: "MusteriEntegrasyon",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MusteriEntegrasyon_Musteri_MusteriId",
                table: "MusteriEntegrasyon",
                column: "MusteriId",
                principalTable: "Musteri",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MusteriEntegrasyon_Musteri_MusteriId",
                table: "MusteriEntegrasyon");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MusteriEntegrasyon",
                table: "MusteriEntegrasyon");

            migrationBuilder.RenameTable(
                name: "MusteriEntegrasyon",
                newName: "FirmaEntegrasyon");

            migrationBuilder.RenameIndex(
                name: "IX_MusteriEntegrasyon_MusteriId",
                table: "FirmaEntegrasyon",
                newName: "IX_FirmaEntegrasyon_MusteriId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FirmaEntegrasyon",
                table: "FirmaEntegrasyon",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FirmaEntegrasyon_Musteri_MusteriId",
                table: "FirmaEntegrasyon",
                column: "MusteriId",
                principalTable: "Musteri",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
