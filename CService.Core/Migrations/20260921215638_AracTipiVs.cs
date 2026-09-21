using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CService.Core.Migrations
{
    /// <inheritdoc />
    public partial class AracTipiVs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Tipi",
                table: "Araclar",
                newName: "AracTipiId");

            migrationBuilder.RenameColumn(
                name: "Marka",
                table: "Araclar",
                newName: "AracMarkaId");

            migrationBuilder.RenameColumn(
                name: "Cinsi",
                table: "Araclar",
                newName: "AracCinsiId");

            migrationBuilder.CreateTable(
                name: "AracCinsleri",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Ad = table.Column<string>(type: "text", nullable: false),
                    Kod = table.Column<string>(type: "text", nullable: true),
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
                    table.PrimaryKey("PK_AracCinsleri", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AracMarkalari",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Ad = table.Column<string>(type: "text", nullable: false),
                    Kod = table.Column<string>(type: "text", nullable: true),
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
                    table.PrimaryKey("PK_AracMarkalari", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AracTipleri",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Ad = table.Column<string>(type: "text", nullable: false),
                    Kod = table.Column<string>(type: "text", nullable: true),
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
                    table.PrimaryKey("PK_AracTipleri", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Araclar_AracCinsiId",
                table: "Araclar",
                column: "AracCinsiId");

            migrationBuilder.CreateIndex(
                name: "IX_Araclar_AracMarkaId",
                table: "Araclar",
                column: "AracMarkaId");

            migrationBuilder.CreateIndex(
                name: "IX_Araclar_AracTipiId",
                table: "Araclar",
                column: "AracTipiId");

            migrationBuilder.AddForeignKey(
                name: "FK_Araclar_AracCinsleri_AracCinsiId",
                table: "Araclar",
                column: "AracCinsiId",
                principalTable: "AracCinsleri",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Araclar_AracMarkalari_AracMarkaId",
                table: "Araclar",
                column: "AracMarkaId",
                principalTable: "AracMarkalari",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Araclar_AracTipleri_AracTipiId",
                table: "Araclar",
                column: "AracTipiId",
                principalTable: "AracTipleri",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Araclar_AracCinsleri_AracCinsiId",
                table: "Araclar");

            migrationBuilder.DropForeignKey(
                name: "FK_Araclar_AracMarkalari_AracMarkaId",
                table: "Araclar");

            migrationBuilder.DropForeignKey(
                name: "FK_Araclar_AracTipleri_AracTipiId",
                table: "Araclar");

            migrationBuilder.DropTable(
                name: "AracCinsleri");

            migrationBuilder.DropTable(
                name: "AracMarkalari");

            migrationBuilder.DropTable(
                name: "AracTipleri");

            migrationBuilder.DropIndex(
                name: "IX_Araclar_AracCinsiId",
                table: "Araclar");

            migrationBuilder.DropIndex(
                name: "IX_Araclar_AracMarkaId",
                table: "Araclar");

            migrationBuilder.DropIndex(
                name: "IX_Araclar_AracTipiId",
                table: "Araclar");

            migrationBuilder.RenameColumn(
                name: "AracTipiId",
                table: "Araclar",
                newName: "Tipi");

            migrationBuilder.RenameColumn(
                name: "AracMarkaId",
                table: "Araclar",
                newName: "Marka");

            migrationBuilder.RenameColumn(
                name: "AracCinsiId",
                table: "Araclar",
                newName: "Cinsi");
        }
    }
}
