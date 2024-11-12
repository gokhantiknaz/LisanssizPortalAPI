using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Humanity.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class carpan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Carpan",
                table: "Abone",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "AylikUTuketimSummaryretimTuketim",
                columns: table => new
                {
                    Aralik = table.Column<double>(type: "double precision", nullable: false),
                    Kasim = table.Column<double>(type: "double precision", nullable: false),
                    Ekim = table.Column<double>(type: "double precision", nullable: false),
                    Eylul = table.Column<double>(type: "double precision", nullable: false),
                    Agustos = table.Column<double>(type: "double precision", nullable: false),
                    Temmuz = table.Column<double>(type: "double precision", nullable: false),
                    Haziran = table.Column<double>(type: "double precision", nullable: false),
                    Mayis = table.Column<double>(type: "double precision", nullable: false),
                    Nisan = table.Column<double>(type: "double precision", nullable: false),
                    Mart = table.Column<double>(type: "double precision", nullable: false),
                    Subat = table.Column<double>(type: "double precision", nullable: false),
                    Ocak = table.Column<double>(type: "double precision", nullable: false),
                    MahsubaDahil = table.Column<bool>(type: "boolean", nullable: false),
                    SeriNo = table.Column<long>(type: "bigint", nullable: false),
                    Firma = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AylikUTuketimSummaryretimTuketim");

            migrationBuilder.DropColumn(
                name: "Carpan",
                table: "Abone");
        }
    }
}
