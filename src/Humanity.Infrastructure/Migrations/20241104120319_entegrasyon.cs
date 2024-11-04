using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Humanity.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class entegrasyon : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_MusteriEntegrasyon_MusteriId",
                table: "MusteriEntegrasyon");

            migrationBuilder.CreateIndex(
                name: "IX_MusteriEntegrasyon_MusteriId",
                table: "MusteriEntegrasyon",
                column: "MusteriId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_MusteriEntegrasyon_MusteriId",
                table: "MusteriEntegrasyon");

            migrationBuilder.CreateIndex(
                name: "IX_MusteriEntegrasyon_MusteriId",
                table: "MusteriEntegrasyon",
                column: "MusteriId",
                unique: true);
        }
    }
}
