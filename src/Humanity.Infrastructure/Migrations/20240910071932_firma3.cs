using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Humanity.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class firma3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Firma_FirmaEntegrasyon_FirmaEntegrasyon",
                table: "Firma");

            migrationBuilder.DropIndex(
                name: "IX_Firma_FirmaEntegrasyon",
                table: "Firma");

            migrationBuilder.DropColumn(
                name: "FirmaEntegrasyon",
                table: "Firma");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FirmaEntegrasyon",
                table: "Firma",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Firma_FirmaEntegrasyon",
                table: "Firma",
                column: "FirmaEntegrasyon",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Firma_FirmaEntegrasyon_FirmaEntegrasyon",
                table: "Firma",
                column: "FirmaEntegrasyon",
                principalTable: "FirmaEntegrasyon",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
