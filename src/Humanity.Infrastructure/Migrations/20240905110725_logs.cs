using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Humanity.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class logs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FirmaEntegrasyon",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirmaId = table.Column<int>(type: "integer", nullable: false),
                    ServisId = table.Column<int>(type: "integer", nullable: false),
                    ServisKullaniciAdi = table.Column<string>(type: "text", nullable: false),
                    ServisSifre = table.Column<string>(type: "text", nullable: false),
                    ServisAdres = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FirmaEntegrasyon", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Logs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Application = table.Column<string>(type: "text", nullable: false),
                    Level = table.Column<string>(type: "text", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Logger = table.Column<string>(type: "text", nullable: false),
                    Exception = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Firma",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    firmaAdi = table.Column<string>(type: "text", nullable: false),
                    FirmaUnvan = table.Column<string>(type: "text", nullable: true),
                    VergiDairesi = table.Column<string>(type: "text", nullable: false),
                    Tckn = table.Column<long>(type: "bigint", nullable: true),
                    Vkn = table.Column<long>(type: "bigint", nullable: true),
                    Durum = table.Column<int>(type: "integer", nullable: false),
                    GercekTuzel = table.Column<int>(type: "integer", nullable: false),
                    OzelkodId1 = table.Column<int>(type: "integer", nullable: true),
                    OzelkodId2 = table.Column<int>(type: "integer", nullable: true),
                    OzelkodId3 = table.Column<int>(type: "integer", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    LastModifiedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModifiedOn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    FirmaEntegrasyon = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Firma", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Firma_FirmaEntegrasyon_FirmaEntegrasyon",
                        column: x => x.FirmaEntegrasyon,
                        principalTable: "FirmaEntegrasyon",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Firma_FirmaEntegrasyon",
                table: "Firma",
                column: "FirmaEntegrasyon");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Firma");

            migrationBuilder.DropTable(
                name: "Logs");

            migrationBuilder.DropTable(
                name: "FirmaEntegrasyon");
        }
    }
}
