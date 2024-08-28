using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Humanity.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class correction3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Abone_UreticiLisans_LisansBilgileriId",
                table: "Abone");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "letisim",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "CepTel",
                table: "letisim",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Adres",
                table: "letisim",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<int>(
                name: "LisansBilgileriId",
                table: "Abone",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.CreateIndex(
                name: "IX_UreticiLisans_MusteriId",
                table: "UreticiLisans",
                column: "MusteriId");

            migrationBuilder.CreateIndex(
                name: "IX_MusteriIletisim_IletisimId",
                table: "MusteriIletisim",
                column: "IletisimId");

            migrationBuilder.CreateIndex(
                name: "IX_AboneIletisim_IletisimId",
                table: "AboneIletisim",
                column: "IletisimId");

            migrationBuilder.AddForeignKey(
                name: "FK_Abone_UreticiLisans_LisansBilgileriId",
                table: "Abone",
                column: "LisansBilgileriId",
                principalTable: "UreticiLisans",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AboneIletisim_letisim_IletisimId",
                table: "AboneIletisim",
                column: "IletisimId",
                principalTable: "letisim",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MusteriIletisim_Musteri_MusteriId",
                table: "MusteriIletisim",
                column: "MusteriId",
                principalTable: "Musteri",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MusteriIletisim_letisim_IletisimId",
                table: "MusteriIletisim",
                column: "IletisimId",
                principalTable: "letisim",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UreticiLisans_Musteri_MusteriId",
                table: "UreticiLisans",
                column: "MusteriId",
                principalTable: "Musteri",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Abone_UreticiLisans_LisansBilgileriId",
                table: "Abone");

            migrationBuilder.DropForeignKey(
                name: "FK_AboneIletisim_letisim_IletisimId",
                table: "AboneIletisim");

            migrationBuilder.DropForeignKey(
                name: "FK_MusteriIletisim_Musteri_MusteriId",
                table: "MusteriIletisim");

            migrationBuilder.DropForeignKey(
                name: "FK_MusteriIletisim_letisim_IletisimId",
                table: "MusteriIletisim");

            migrationBuilder.DropForeignKey(
                name: "FK_UreticiLisans_Musteri_MusteriId",
                table: "UreticiLisans");

            migrationBuilder.DropIndex(
                name: "IX_UreticiLisans_MusteriId",
                table: "UreticiLisans");

            migrationBuilder.DropIndex(
                name: "IX_MusteriIletisim_IletisimId",
                table: "MusteriIletisim");

            migrationBuilder.DropIndex(
                name: "IX_AboneIletisim_IletisimId",
                table: "AboneIletisim");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "letisim",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CepTel",
                table: "letisim",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Adres",
                table: "letisim",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "LisansBilgileriId",
                table: "Abone",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Abone_UreticiLisans_LisansBilgileriId",
                table: "Abone",
                column: "LisansBilgileriId",
                principalTable: "UreticiLisans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
