using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CService.Core.Migrations
{
    /// <inheritdoc />
    public partial class FirmaHostedId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HostesAdi",
                table: "Araclar");

            migrationBuilder.DropColumn(
                name: "HostesKimlikNo",
                table: "Araclar");

            migrationBuilder.DropColumn(
                name: "HostesTelefon",
                table: "Araclar");

            migrationBuilder.AddColumn<int>(
                name: "HostesId",
                table: "Araclar",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Araclar_HostesId",
                table: "Araclar",
                column: "HostesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Araclar_Hosteler_HostesId",
                table: "Araclar",
                column: "HostesId",
                principalTable: "Hosteler",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Araclar_Hosteler_HostesId",
                table: "Araclar");

            migrationBuilder.DropIndex(
                name: "IX_Araclar_HostesId",
                table: "Araclar");

            migrationBuilder.DropColumn(
                name: "HostesId",
                table: "Araclar");

            migrationBuilder.AddColumn<string>(
                name: "HostesAdi",
                table: "Araclar",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HostesKimlikNo",
                table: "Araclar",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HostesTelefon",
                table: "Araclar",
                type: "text",
                nullable: true);
        }
    }
}
