using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Humanity.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class correction1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AboneIletisim_letisim_IletisimId",
                table: "AboneIletisim");

            migrationBuilder.DropForeignKey(
                name: "FK_MusteriIletisim_letisim_IletisimId",
                table: "MusteriIletisim");

            migrationBuilder.DropPrimaryKey(
                name: "PK_letisim",
                table: "letisim");

            migrationBuilder.RenameTable(
                name: "letisim",
                newName: "Iletisim");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Iletisim",
                table: "Iletisim",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "CariIletisim",
                columns: table => new
                {
                    IletisimId = table.Column<int>(type: "integer", nullable: false),
                    CariKartId = table.Column<int>(type: "integer", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    LastModifiedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModifiedOn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CariIletisim", x => new { x.CariKartId, x.IletisimId });
                    table.ForeignKey(
                        name: "FK_CariIletisim_CariKart_CariKartId",
                        column: x => x.CariKartId,
                        principalTable: "CariKart",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CariIletisim_Iletisim_IletisimId",
                        column: x => x.IletisimId,
                        principalTable: "Iletisim",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CariIletisim_IletisimId",
                table: "CariIletisim",
                column: "IletisimId");

            migrationBuilder.AddForeignKey(
                name: "FK_AboneIletisim_Iletisim_IletisimId",
                table: "AboneIletisim",
                column: "IletisimId",
                principalTable: "Iletisim",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MusteriIletisim_Iletisim_IletisimId",
                table: "MusteriIletisim",
                column: "IletisimId",
                principalTable: "Iletisim",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AboneIletisim_Iletisim_IletisimId",
                table: "AboneIletisim");

            migrationBuilder.DropForeignKey(
                name: "FK_MusteriIletisim_Iletisim_IletisimId",
                table: "MusteriIletisim");

            migrationBuilder.DropTable(
                name: "CariIletisim");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Iletisim",
                table: "Iletisim");

            migrationBuilder.RenameTable(
                name: "Iletisim",
                newName: "letisim");

            migrationBuilder.AddPrimaryKey(
                name: "PK_letisim",
                table: "letisim",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AboneIletisim_letisim_IletisimId",
                table: "AboneIletisim",
                column: "IletisimId",
                principalTable: "letisim",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MusteriIletisim_letisim_IletisimId",
                table: "MusteriIletisim",
                column: "IletisimId",
                principalTable: "letisim",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
