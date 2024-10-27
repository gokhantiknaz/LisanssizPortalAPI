using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Humanity.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class aboneaylikendeks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AboneAylikEndeks");

            migrationBuilder.CreateTable(
                name: "AboneEndeks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AboneId = table.Column<int>(type: "integer", nullable: false),
                    EndexYear = table.Column<int>(type: "integer", nullable: false),
                    EndexMonth = table.Column<int>(type: "integer", nullable: false),
                    EndexDate = table.Column<long>(type: "bigint", nullable: false),
                    EndexType = table.Column<int>(type: "integer", nullable: false),
                    T1Endex = table.Column<double>(type: "double precision", nullable: false),
                    T2Endex = table.Column<double>(type: "double precision", nullable: false),
                    T3Endex = table.Column<double>(type: "double precision", nullable: false),
                    T4Endex = table.Column<double>(type: "double precision", nullable: false),
                    ReactiveCapasitive = table.Column<double>(type: "double precision", nullable: false),
                    MaxDemand = table.Column<double>(type: "double precision", nullable: false),
                    DemandDate = table.Column<long>(type: "bigint", nullable: false),
                    TSum = table.Column<double>(type: "double precision", nullable: false),
                    ReactiveInductive = table.Column<double>(type: "double precision", nullable: false),
                    UpdateMongoID = table.Column<string>(type: "text", nullable: false),
                    SensorSerno = table.Column<int>(type: "integer", nullable: false),
                    SensorIdentifier = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AboneEndeks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AboneEndeks_Abone_AboneId",
                        column: x => x.AboneId,
                        principalTable: "Abone",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AboneEndeks_AboneId",
                table: "AboneEndeks",
                column: "AboneId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AboneEndeks");

            migrationBuilder.CreateTable(
                name: "AboneAylikEndeks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AboneId = table.Column<int>(type: "integer", nullable: false),
                    Carpan = table.Column<decimal>(type: "numeric", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    Donem = table.Column<string>(type: "text", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    LastModifiedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModifiedOn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    TotalReakIndCekis = table.Column<decimal>(type: "numeric", nullable: false),
                    TotalReakIndVeris = table.Column<decimal>(type: "numeric", nullable: false),
                    TotalReakKapCekis = table.Column<decimal>(type: "numeric", nullable: false),
                    TotalReakKapVeris = table.Column<decimal>(type: "numeric", nullable: false),
                    TotalTuketimCekis = table.Column<decimal>(type: "numeric", nullable: false),
                    TotalUretimVeris = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AboneAylikEndeks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AboneAylikEndeks_Abone_AboneId",
                        column: x => x.AboneId,
                        principalTable: "Abone",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AboneAylikEndeks_AboneId",
                table: "AboneAylikEndeks",
                column: "AboneId");
        }
    }
}
