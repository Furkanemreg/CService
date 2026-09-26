using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CService.Core.Migrations
{
    /// <inheritdoc />
    public partial class YetkiliAndHostesDetail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Yetkililer",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TcKimlikNo",
                table: "Yetkililer",
                type: "character varying(11)",
                maxLength: 11,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Tel",
                table: "Yetkililer",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Hosteler",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TcKimlikNo",
                table: "Hosteler",
                type: "character varying(11)",
                maxLength: 11,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Tel",
                table: "Hosteler",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Yetkililer");

            migrationBuilder.DropColumn(
                name: "TcKimlikNo",
                table: "Yetkililer");

            migrationBuilder.DropColumn(
                name: "Tel",
                table: "Yetkililer");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Hosteler");

            migrationBuilder.DropColumn(
                name: "TcKimlikNo",
                table: "Hosteler");

            migrationBuilder.DropColumn(
                name: "Tel",
                table: "Hosteler");
        }
    }
}
