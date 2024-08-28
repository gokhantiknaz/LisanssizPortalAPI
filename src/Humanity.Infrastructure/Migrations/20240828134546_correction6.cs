using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Humanity.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class correction6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Abone_AboneSayac_AboneSayacId",
                table: "Abone");

            migrationBuilder.DropIndex(
                name: "IX_UreticiLisans_AboneId",
                table: "UreticiLisans");

            migrationBuilder.DropIndex(
                name: "IX_AboneIletisim_AboneId",
                table: "AboneIletisim");

            migrationBuilder.DropIndex(
                name: "IX_Abone_AboneSayacId",
                table: "Abone");

            migrationBuilder.DropColumn(
                name: "AboneSayacId",
                table: "Abone");

            migrationBuilder.RenameColumn(
                name: "MusteriId",
                table: "AboneSayac",
                newName: "AboneId");

            migrationBuilder.CreateIndex(
                name: "IX_UreticiLisans_AboneId",
                table: "UreticiLisans",
                column: "AboneId");

            migrationBuilder.CreateIndex(
                name: "IX_AboneSayac_AboneId",
                table: "AboneSayac",
                column: "AboneId");

            migrationBuilder.AddForeignKey(
                name: "FK_AboneSayac_Abone_AboneId",
                table: "AboneSayac",
                column: "AboneId",
                principalTable: "Abone",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AboneSayac_Abone_AboneId",
                table: "AboneSayac");

            migrationBuilder.DropIndex(
                name: "IX_UreticiLisans_AboneId",
                table: "UreticiLisans");

            migrationBuilder.DropIndex(
                name: "IX_AboneSayac_AboneId",
                table: "AboneSayac");

            migrationBuilder.RenameColumn(
                name: "AboneId",
                table: "AboneSayac",
                newName: "MusteriId");

            migrationBuilder.AddColumn<int>(
                name: "AboneSayacId",
                table: "Abone",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UreticiLisans_AboneId",
                table: "UreticiLisans",
                column: "AboneId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AboneIletisim_AboneId",
                table: "AboneIletisim",
                column: "AboneId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Abone_AboneSayacId",
                table: "Abone",
                column: "AboneSayacId");

            migrationBuilder.AddForeignKey(
                name: "FK_Abone_AboneSayac_AboneSayacId",
                table: "Abone",
                column: "AboneSayacId",
                principalTable: "AboneSayac",
                principalColumn: "Id");
        }
    }
}
