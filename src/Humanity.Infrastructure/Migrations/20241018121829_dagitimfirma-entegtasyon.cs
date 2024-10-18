using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Humanity.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class dagitimfirmaentegtasyon : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FirmaEntegrasyon_Firma_FirmaId",
                table: "FirmaEntegrasyon");

            migrationBuilder.DropIndex(
                name: "IX_FirmaEntegrasyon_FirmaId",
                table: "FirmaEntegrasyon");

            migrationBuilder.RenameColumn(
                name: "ServisSifre",
                table: "FirmaEntegrasyon",
                newName: "Sifre");

            migrationBuilder.RenameColumn(
                name: "ServisKullaniciAdi",
                table: "FirmaEntegrasyon",
                newName: "KullaniciAdi");

            migrationBuilder.RenameColumn(
                name: "FirmaId",
                table: "FirmaEntegrasyon",
                newName: "MusteriId");

            migrationBuilder.AddColumn<int>(
                name: "DagitimFirmaId",
                table: "Musteri",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "ServisAdres",
                table: "FirmaEntegrasyon",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.CreateIndex(
                name: "IX_FirmaEntegrasyon_MusteriId",
                table: "FirmaEntegrasyon",
                column: "MusteriId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_FirmaEntegrasyon_Musteri_MusteriId",
                table: "FirmaEntegrasyon",
                column: "MusteriId",
                principalTable: "Musteri",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FirmaEntegrasyon_Musteri_MusteriId",
                table: "FirmaEntegrasyon");

            migrationBuilder.DropIndex(
                name: "IX_FirmaEntegrasyon_MusteriId",
                table: "FirmaEntegrasyon");

            migrationBuilder.DropColumn(
                name: "DagitimFirmaId",
                table: "Musteri");

            migrationBuilder.RenameColumn(
                name: "Sifre",
                table: "FirmaEntegrasyon",
                newName: "ServisSifre");

            migrationBuilder.RenameColumn(
                name: "MusteriId",
                table: "FirmaEntegrasyon",
                newName: "FirmaId");

            migrationBuilder.RenameColumn(
                name: "KullaniciAdi",
                table: "FirmaEntegrasyon",
                newName: "ServisKullaniciAdi");

            migrationBuilder.AlterColumn<string>(
                name: "ServisAdres",
                table: "FirmaEntegrasyon",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FirmaEntegrasyon_FirmaId",
                table: "FirmaEntegrasyon",
                column: "FirmaId");

            migrationBuilder.AddForeignKey(
                name: "FK_FirmaEntegrasyon_Firma_FirmaId",
                table: "FirmaEntegrasyon",
                column: "FirmaId",
                principalTable: "Firma",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
