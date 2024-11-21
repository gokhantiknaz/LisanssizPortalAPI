using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Humanity.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class endeksveridurumu2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OwnerConsumpiton");

            migrationBuilder.CreateTable(
                name: "AylikEnYuksekEnDusukTuketimGunveMiktar",
                columns: table => new
                {
                    Donem = table.Column<string>(type: "text", nullable: false),
                    HighConsumption = table.Column<decimal>(type: "numeric", nullable: false),
                    HighDay = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LowConsumption = table.Column<decimal>(type: "numeric", nullable: false),
                    LowDay = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "DailyProductionConsumption",
                columns: table => new
                {
                    Donem = table.Column<string>(type: "text", nullable: false),
                    Gun = table.Column<int>(type: "integer", nullable: false),
                    ToplamUretim = table.Column<double>(type: "double precision", nullable: false),
                    ToplamTuketim = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "EndeksVeriDurumu",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Tarih = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    VeriCekildi = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EndeksVeriDurumu", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AylikEnYuksekEnDusukTuketimGunveMiktar");

            migrationBuilder.DropTable(
                name: "DailyProductionConsumption");

            migrationBuilder.DropTable(
                name: "EndeksVeriDurumu");

            migrationBuilder.CreateTable(
                name: "OwnerConsumpiton",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Donem = table.Column<string>(type: "text", nullable: false),
                    Firma = table.Column<string>(type: "text", nullable: false),
                    Json = table.Column<string>(type: "jsonb", nullable: false),
                    SerNo = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OwnerConsumpiton", x => x.Id);
                });
        }
    }
}
