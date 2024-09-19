using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Humanity.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class aylikendeks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MusteriAylikEndeks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MusteriId = table.Column<int>(type: "integer", nullable: false),
                    Donem = table.Column<string>(type: "text", nullable: false),
                    TotalTuketimCekis = table.Column<decimal>(type: "numeric", nullable: false),
                    TotalUretimVeris = table.Column<decimal>(type: "numeric", nullable: false),
                    TotalReakIndVeris = table.Column<decimal>(type: "numeric", nullable: false),
                    TotalReakKapVeris = table.Column<decimal>(type: "numeric", nullable: false),
                    TotalReakIndCekis = table.Column<decimal>(type: "numeric", nullable: false),
                    TotalReakKapCekis = table.Column<decimal>(type: "numeric", nullable: false),
                    Carpan = table.Column<decimal>(type: "numeric", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    LastModifiedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModifiedOn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MusteriAylikEndeks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MusteriAylikEndeks_Musteri_MusteriId",
                        column: x => x.MusteriId,
                        principalTable: "Musteri",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MusteriAylikEndeks_MusteriId",
                table: "MusteriAylikEndeks",
                column: "MusteriId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MusteriAylikEndeks");
        }
    }
}
