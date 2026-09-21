using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CService.Core.Migrations
{
    /// <inheritdoc />
    public partial class OkulOdemeTipi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OkulOdemeTpi",
                table: "Firmalar",
                type: "integer",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OkulOdemeTpi",
                table: "Firmalar");
        }
    }
}
