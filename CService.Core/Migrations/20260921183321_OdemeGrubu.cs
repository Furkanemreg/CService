using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CService.Core.Migrations
{
    /// <inheritdoc />
    public partial class OdemeGrubu : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OdemeGrubuId",
                table: "AracSahipleri",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "OdemeGruplari",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Ad = table.Column<string>(type: "text", nullable: false),
                    Kod = table.Column<string>(type: "text", nullable: false),
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
                    table.PrimaryKey("PK_OdemeGruplari", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AracSahipleri_OdemeGrubuId",
                table: "AracSahipleri",
                column: "OdemeGrubuId");

            migrationBuilder.AddForeignKey(
                name: "FK_AracSahipleri_OdemeGruplari_OdemeGrubuId",
                table: "AracSahipleri",
                column: "OdemeGrubuId",
                principalTable: "OdemeGruplari",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AracSahipleri_OdemeGruplari_OdemeGrubuId",
                table: "AracSahipleri");

            migrationBuilder.DropTable(
                name: "OdemeGruplari");

            migrationBuilder.DropIndex(
                name: "IX_AracSahipleri_OdemeGrubuId",
                table: "AracSahipleri");

            migrationBuilder.DropColumn(
                name: "OdemeGrubuId",
                table: "AracSahipleri");
        }
    }
}
