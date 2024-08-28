using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Humanity.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class correction4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Abone_UreticiLisans_LisansBilgileriId",
                table: "Abone");

            migrationBuilder.DropForeignKey(
                name: "FK_UreticiLisans_Musteri_MusteriId",
                table: "UreticiLisans");

            migrationBuilder.DropIndex(
                name: "IX_UreticiLisans_MusteriId",
                table: "UreticiLisans");

            migrationBuilder.DropIndex(
                name: "IX_Abone_LisansBilgileriId",
                table: "Abone");

            migrationBuilder.DropColumn(
                name: "LisansBilgileriId",
                table: "Abone");

            migrationBuilder.RenameColumn(
                name: "MusteriId",
                table: "UreticiLisans",
                newName: "AboneId");

            migrationBuilder.CreateIndex(
                name: "IX_UreticiLisans_AboneId",
                table: "UreticiLisans",
                column: "AboneId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_UreticiLisans_Abone_AboneId",
                table: "UreticiLisans",
                column: "AboneId",
                principalTable: "Abone",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UreticiLisans_Abone_AboneId",
                table: "UreticiLisans");

            migrationBuilder.DropIndex(
                name: "IX_UreticiLisans_AboneId",
                table: "UreticiLisans");

            migrationBuilder.RenameColumn(
                name: "AboneId",
                table: "UreticiLisans",
                newName: "MusteriId");

            migrationBuilder.AddColumn<int>(
                name: "LisansBilgileriId",
                table: "Abone",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UreticiLisans_MusteriId",
                table: "UreticiLisans",
                column: "MusteriId");

            migrationBuilder.CreateIndex(
                name: "IX_Abone_LisansBilgileriId",
                table: "Abone",
                column: "LisansBilgileriId");

            migrationBuilder.AddForeignKey(
                name: "FK_Abone_UreticiLisans_LisansBilgileriId",
                table: "Abone",
                column: "LisansBilgileriId",
                principalTable: "UreticiLisans",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UreticiLisans_Musteri_MusteriId",
                table: "UreticiLisans",
                column: "MusteriId",
                principalTable: "Musteri",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
