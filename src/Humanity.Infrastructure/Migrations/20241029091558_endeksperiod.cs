using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Humanity.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class endeksperiod : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AboneAylikTuketim",
                columns: table => new
                {
                    Unvan = table.Column<string>(type: "text", nullable: false),
                    EndexMonth = table.Column<int>(type: "integer", nullable: false),
                    EndexYear = table.Column<int>(type: "integer", nullable: false),
                    T1Usage = table.Column<double>(type: "double precision", nullable: false),
                    T2Usage = table.Column<double>(type: "double precision", nullable: false),
                    T3Usage = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "AboneEndeksPeriod",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AboneEndeksId = table.Column<int>(type: "integer", nullable: false),
                    EnerijCinsi = table.Column<int>(type: "integer", nullable: false),
                    EneriTur = table.Column<int>(type: "integer", nullable: false),
                    EndeksDirection = table.Column<int>(type: "integer", nullable: false),
                    IlkEndeks = table.Column<double>(type: "double precision", nullable: false),
                    SonEndeks = table.Column<double>(type: "double precision", nullable: false),
                    Toplam = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AboneEndeksPeriod", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AboneEndeksPeriod_AboneEndeks_AboneEndeksId",
                        column: x => x.AboneEndeksId,
                        principalTable: "AboneEndeks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AboneEndeksPeriod_AboneEndeksId",
                table: "AboneEndeksPeriod",
                column: "AboneEndeksId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AboneAylikTuketim");

            migrationBuilder.DropTable(
                name: "AboneEndeksPeriod");
        }
    }
}
