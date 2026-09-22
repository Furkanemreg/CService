using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CService.Core.Migrations
{
    /// <inheritdoc />
    public partial class LanetliOdemeTipi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OdemeTipi",
                table: "AracSahipleri");

            migrationBuilder.AddColumn<int>(
                name: "OdemeTipiId",
                table: "AracSahipleri",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "OdemeTipleri",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Kod = table.Column<string>(type: "text", nullable: false),
                    Ad = table.Column<string>(type: "text", nullable: false),
                    Kdv = table.Column<int>(type: "integer", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<string>(type: "text", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedBy = table.Column<string>(type: "text", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OdemeTipleri", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AracSahipleri_OdemeTipiId",
                table: "AracSahipleri",
                column: "OdemeTipiId");

            migrationBuilder.AddForeignKey(
                name: "FK_AracSahipleri_OdemeTipleri_OdemeTipiId",
                table: "AracSahipleri",
                column: "OdemeTipiId",
                principalTable: "OdemeTipleri",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AracSahipleri_OdemeTipleri_OdemeTipiId",
                table: "AracSahipleri");

            migrationBuilder.DropTable(
                name: "OdemeTipleri");

            migrationBuilder.DropIndex(
                name: "IX_AracSahipleri_OdemeTipiId",
                table: "AracSahipleri");

            migrationBuilder.DropColumn(
                name: "OdemeTipiId",
                table: "AracSahipleri");

            migrationBuilder.AddColumn<int>(
                name: "OdemeTipi",
                table: "AracSahipleri",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
