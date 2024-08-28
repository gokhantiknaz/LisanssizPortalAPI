using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Humanity.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class correction2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Abone_MusteriId",
                table: "Abone",
                column: "MusteriId");

            migrationBuilder.AddForeignKey(
                name: "FK_Abone_Musteri_MusteriId",
                table: "Abone",
                column: "MusteriId",
                principalTable: "Musteri",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Abone_Musteri_MusteriId",
                table: "Abone");

            migrationBuilder.DropIndex(
                name: "IX_Abone_MusteriId",
                table: "Abone");
        }
    }
}
