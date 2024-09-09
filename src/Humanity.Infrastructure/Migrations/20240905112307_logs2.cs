using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Humanity.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class logs2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Callsite",
                table: "Logs",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Message",
                table: "Logs",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Callsite",
                table: "Logs");

            migrationBuilder.DropColumn(
                name: "Message",
                table: "Logs");
        }
    }
}
