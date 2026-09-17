using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CService.Core.Migrations
{
    /// <inheritdoc />
    public partial class FirmaMail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Mail",
                table: "Firmalar",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Mail",
                table: "Firmalar");
        }
    }
}
