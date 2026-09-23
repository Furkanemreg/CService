using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CService.Core.Migrations
{
    /// <inheritdoc />
    public partial class GuzergahTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Guzergahlar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirmaId = table.Column<int>(type: "integer", nullable: false),
                    Kod = table.Column<string>(type: "text", nullable: false),
                    Kod2 = table.Column<string>(type: "text", nullable: true),
                    Ad = table.Column<string>(type: "text", nullable: false),
                    BolgeId = table.Column<int>(type: "integer", nullable: true),
                    YetkiliId = table.Column<int>(type: "integer", nullable: true),
                    Ay = table.Column<int>(type: "integer", nullable: false),
                    Yil = table.Column<int>(type: "integer", nullable: false),
                    Aciklama = table.Column<string>(type: "text", nullable: true),
                    HostesId = table.Column<int>(type: "integer", nullable: true),
                    Kapasite = table.Column<int>(type: "integer", nullable: true),
                    KmTekYon = table.Column<decimal>(type: "numeric", nullable: true),
                    SeferSayisi = table.Column<int>(type: "integer", nullable: true),
                    ServisIstikameti = table.Column<string>(type: "text", nullable: true),
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
                    table.PrimaryKey("PK_Guzergahlar", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Guzergahlar_Bolgeler_BolgeId",
                        column: x => x.BolgeId,
                        principalTable: "Bolgeler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Guzergahlar_Firmalar_FirmaId",
                        column: x => x.FirmaId,
                        principalTable: "Firmalar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Guzergahlar_Hosteler_HostesId",
                        column: x => x.HostesId,
                        principalTable: "Hosteler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Guzergahlar_Yetkililer_YetkiliId",
                        column: x => x.YetkiliId,
                        principalTable: "Yetkililer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Guzergahlar_BolgeId",
                table: "Guzergahlar",
                column: "BolgeId");

            migrationBuilder.CreateIndex(
                name: "IX_Guzergahlar_FirmaId",
                table: "Guzergahlar",
                column: "FirmaId");

            migrationBuilder.CreateIndex(
                name: "IX_Guzergahlar_HostesId",
                table: "Guzergahlar",
                column: "HostesId");

            migrationBuilder.CreateIndex(
                name: "IX_Guzergahlar_YetkiliId",
                table: "Guzergahlar",
                column: "YetkiliId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Guzergahlar");
        }
    }
}
