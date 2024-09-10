using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Humanity.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class firm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Firma_FirmaEntegrasyon",
                table: "Firma");

            migrationBuilder.RenameColumn(
                name: "firmaAdi",
                table: "Firma",
                newName: "FirmaAdi");

            migrationBuilder.AddColumn<string>(
                name: "SorumluAd",
                table: "Firma",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SorumluEmail",
                table: "Firma",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SorumluSoyad",
                table: "Firma",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SorumluTelefon",
                table: "Firma",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "FirmaIletisim",
                columns: table => new
                {
                    IletisimId = table.Column<int>(type: "integer", nullable: false),
                    FirmaId = table.Column<int>(type: "integer", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    LastModifiedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModifiedOn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FirmaIletisim", x => new { x.FirmaId, x.IletisimId });
                    table.ForeignKey(
                        name: "FK_FirmaIletisim_Firma_FirmaId",
                        column: x => x.FirmaId,
                        principalTable: "Firma",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FirmaIletisim_Iletisim_IletisimId",
                        column: x => x.IletisimId,
                        principalTable: "Iletisim",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FirmaEntegrasyon_FirmaId",
                table: "FirmaEntegrasyon",
                column: "FirmaId");

            migrationBuilder.CreateIndex(
                name: "IX_Firma_FirmaEntegrasyon",
                table: "Firma",
                column: "FirmaEntegrasyon",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FirmaIletisim_IletisimId",
                table: "FirmaIletisim",
                column: "IletisimId");

            migrationBuilder.AddForeignKey(
                name: "FK_FirmaEntegrasyon_Firma_FirmaId",
                table: "FirmaEntegrasyon",
                column: "FirmaId",
                principalTable: "Firma",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FirmaEntegrasyon_Firma_FirmaId",
                table: "FirmaEntegrasyon");

            migrationBuilder.DropTable(
                name: "FirmaIletisim");

            migrationBuilder.DropIndex(
                name: "IX_FirmaEntegrasyon_FirmaId",
                table: "FirmaEntegrasyon");

            migrationBuilder.DropIndex(
                name: "IX_Firma_FirmaEntegrasyon",
                table: "Firma");

            migrationBuilder.DropColumn(
                name: "SorumluAd",
                table: "Firma");

            migrationBuilder.DropColumn(
                name: "SorumluEmail",
                table: "Firma");

            migrationBuilder.DropColumn(
                name: "SorumluSoyad",
                table: "Firma");

            migrationBuilder.DropColumn(
                name: "SorumluTelefon",
                table: "Firma");

            migrationBuilder.RenameColumn(
                name: "FirmaAdi",
                table: "Firma",
                newName: "firmaAdi");

            migrationBuilder.CreateIndex(
                name: "IX_Firma_FirmaEntegrasyon",
                table: "Firma",
                column: "FirmaEntegrasyon");
        }
    }
}
