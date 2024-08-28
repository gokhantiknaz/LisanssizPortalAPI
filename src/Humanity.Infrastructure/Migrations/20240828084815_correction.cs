using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Humanity.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class correction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Abone_UreitciLisans_LisansBilgileriId",
                table: "Abone");

            migrationBuilder.DropTable(
                name: "UreitciLisans");

            migrationBuilder.CreateTable(
                name: "UreticiLisans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MusteriId = table.Column<int>(type: "integer", nullable: false),
                    UretimSekli = table.Column<int>(type: "integer", nullable: false),
                    LisansBilgisi = table.Column<int>(type: "integer", nullable: false),
                    UretimBaslama = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CagrimektupTarihi = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    MahsupTipi = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UreticiLisans", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Abone_UreticiLisans_LisansBilgileriId",
                table: "Abone",
                column: "LisansBilgileriId",
                principalTable: "UreticiLisans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Abone_UreticiLisans_LisansBilgileriId",
                table: "Abone");

            migrationBuilder.DropTable(
                name: "UreticiLisans");

            migrationBuilder.CreateTable(
                name: "UreitciLisans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CagrimektupTarihi = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LisansBilgisi = table.Column<int>(type: "integer", nullable: false),
                    MahsupTipi = table.Column<int>(type: "integer", nullable: false),
                    MusteriId = table.Column<int>(type: "integer", nullable: false),
                    UretimBaslama = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UretimSekli = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UreitciLisans", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Abone_UreitciLisans_LisansBilgileriId",
                table: "Abone",
                column: "LisansBilgileriId",
                principalTable: "UreitciLisans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
