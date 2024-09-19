using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Humanity.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class aktarim : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MusteriSaatlikEndeks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MusteriId = table.Column<int>(type: "integer", nullable: false),
                    ProfilDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CekisTuketim = table.Column<decimal>(type: "numeric", nullable: false),
                    CekisReaktifInduktif = table.Column<decimal>(type: "numeric", nullable: false),
                    CekisReaktifKapasitif = table.Column<decimal>(type: "numeric", nullable: false),
                    Uretim = table.Column<decimal>(type: "numeric", nullable: false),
                    VerisReaktifInduktif = table.Column<decimal>(type: "numeric", nullable: false),
                    VerisReaktifKapasitif = table.Column<decimal>(type: "numeric", nullable: false),
                    Carpan = table.Column<decimal>(type: "numeric", nullable: false),
                    Donem = table.Column<string>(type: "text", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    LastModifiedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModifiedOn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MusteriSaatlikEndeks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MusteriSaatlikEndeks_Musteri_MusteriId",
                        column: x => x.MusteriId,
                        principalTable: "Musteri",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MusteriSaatlikEndeks_MusteriId",
                table: "MusteriSaatlikEndeks",
                column: "MusteriId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MusteriSaatlikEndeks");
        }
    }
}
