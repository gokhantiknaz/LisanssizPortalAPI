using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Humanity.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class profildateremove : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfilDate",
                table: "AboneSaatlikEndeks");

            migrationBuilder.AlterColumn<string>(
                name: "Firma",
                table: "AylikUTuketimSummaryretimTuketim",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<double>(
                name: "AralikOncekiYil",
                table: "AylikUTuketimSummaryretimTuketim",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<decimal>(
                name: "Carpan",
                table: "AylikUTuketimSummaryretimTuketim",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "EndexType",
                table: "AboneAylikTuketim",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SeriNo",
                table: "AboneAylikTuketim",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "AylikUretimTuketim",
                columns: table => new
                {
                    Tuketim = table.Column<double>(type: "double precision", nullable: false),
                    TuketimMahsubaDahilDegil = table.Column<double>(type: "double precision", nullable: false),
                    Uretim = table.Column<double>(type: "double precision", nullable: false),
                    EndexMonth = table.Column<int>(type: "integer", nullable: false),
                    EndexYear = table.Column<int>(type: "integer", nullable: false),
                    T1Tuketim = table.Column<double>(type: "double precision", nullable: true),
                    T2Tuketim = table.Column<double>(type: "double precision", nullable: true),
                    T3Tuketim = table.Column<double>(type: "double precision", nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "YillikUretimTuketim",
                columns: table => new
                {
                    TotalEndex = table.Column<double>(type: "double precision", nullable: false),
                    EndexType = table.Column<int>(type: "integer", nullable: false),
                    Unvan = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AylikUretimTuketim");

            migrationBuilder.DropTable(
                name: "YillikUretimTuketim");

            migrationBuilder.DropColumn(
                name: "AralikOncekiYil",
                table: "AylikUTuketimSummaryretimTuketim");

            migrationBuilder.DropColumn(
                name: "Carpan",
                table: "AylikUTuketimSummaryretimTuketim");

            migrationBuilder.DropColumn(
                name: "EndexType",
                table: "AboneAylikTuketim");

            migrationBuilder.DropColumn(
                name: "SeriNo",
                table: "AboneAylikTuketim");

            migrationBuilder.AlterColumn<int>(
                name: "Firma",
                table: "AylikUTuketimSummaryretimTuketim",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "ProfilDate",
                table: "AboneSaatlikEndeks",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));
        }
    }
}
