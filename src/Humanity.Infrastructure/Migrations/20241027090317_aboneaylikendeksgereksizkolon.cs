using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Humanity.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class aboneaylikendeksgereksizkolon : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SensorIdentifier",
                table: "AboneEndeks");

            migrationBuilder.DropColumn(
                name: "UpdateMongoID",
                table: "AboneEndeks");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SensorIdentifier",
                table: "AboneEndeks",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UpdateMongoID",
                table: "AboneEndeks",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
