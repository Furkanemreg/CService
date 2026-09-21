using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CService.Core.Migrations
{
    /// <inheritdoc />
    public partial class AracVs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AracSahipleri",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Kod = table.Column<string>(type: "text", nullable: false),
                    Adi = table.Column<string>(type: "text", nullable: false),
                    Soyad = table.Column<string>(type: "text", nullable: false),
                    TcKimlikNo = table.Column<string>(type: "character varying(11)", maxLength: 11, nullable: true),
                    Tel = table.Column<string>(type: "text", nullable: true),
                    Adres = table.Column<string>(type: "text", nullable: true),
                    VergiDairesi = table.Column<string>(type: "text", nullable: true),
                    VergiNo = table.Column<string>(type: "text", nullable: true),
                    Vekil = table.Column<string>(type: "text", nullable: true),
                    VekilTel = table.Column<string>(type: "text", nullable: true),
                    Not = table.Column<string>(type: "text", nullable: true),
                    Sorumlu = table.Column<string>(type: "text", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    OdemeTipi = table.Column<int>(type: "integer", nullable: false),
                    VergiUsulu = table.Column<int>(type: "integer", nullable: false),
                    BankaHesabiId = table.Column<int>(type: "integer", nullable: true),
                    HesapSahibi = table.Column<string>(type: "text", nullable: true),
                    HesapNo = table.Column<string>(type: "text", nullable: true),
                    Iban = table.Column<string>(type: "text", nullable: true),
                    YakitOrani = table.Column<decimal>(type: "numeric", nullable: true),
                    OkulKomisyonu = table.Column<decimal>(type: "numeric", nullable: true),
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
                    table.PrimaryKey("PK_AracSahipleri", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AracSahipleri_BankaHesaplari_BankaHesabiId",
                        column: x => x.BankaHesabiId,
                        principalTable: "BankaHesaplari",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Araclar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Plaka = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    OdemeDurumu = table.Column<int>(type: "integer", nullable: false),
                    Cinsi = table.Column<int>(type: "integer", nullable: false),
                    Marka = table.Column<int>(type: "integer", nullable: false),
                    Tipi = table.Column<int>(type: "integer", nullable: false),
                    Modeli = table.Column<string>(type: "text", nullable: true),
                    Kapasite = table.Column<int>(type: "integer", nullable: true),
                    RuhsatNo = table.Column<string>(type: "text", nullable: true),
                    MotorNo = table.Column<string>(type: "text", nullable: true),
                    SasiNo = table.Column<string>(type: "text", nullable: true),
                    Klima = table.Column<bool>(type: "boolean", nullable: false),
                    IlkGirisTarihi = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    RuhsatSahibi = table.Column<string>(type: "text", nullable: true),
                    RuhsatSahibiKimlikNo = table.Column<string>(type: "text", nullable: true),
                    SoforAdi = table.Column<string>(type: "text", nullable: true),
                    SoforKimlikNo = table.Column<string>(type: "text", nullable: true),
                    SoforTelefon = table.Column<string>(type: "text", nullable: true),
                    HostesAdi = table.Column<string>(type: "text", nullable: true),
                    HostesKimlikNo = table.Column<string>(type: "text", nullable: true),
                    HostesTelefon = table.Column<string>(type: "text", nullable: true),
                    FirmaId = table.Column<int>(type: "integer", nullable: false),
                    AracSahibiId = table.Column<int>(type: "integer", nullable: false),
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
                    table.PrimaryKey("PK_Araclar", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Araclar_AracSahipleri_AracSahibiId",
                        column: x => x.AracSahibiId,
                        principalTable: "AracSahipleri",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Araclar_Firmalar_FirmaId",
                        column: x => x.FirmaId,
                        principalTable: "Firmalar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Araclar_AracSahibiId",
                table: "Araclar",
                column: "AracSahibiId");

            migrationBuilder.CreateIndex(
                name: "IX_Araclar_FirmaId",
                table: "Araclar",
                column: "FirmaId");

            migrationBuilder.CreateIndex(
                name: "IX_AracSahipleri_BankaHesabiId",
                table: "AracSahipleri",
                column: "BankaHesabiId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Araclar");

            migrationBuilder.DropTable(
                name: "AracSahipleri");
        }
    }
}
