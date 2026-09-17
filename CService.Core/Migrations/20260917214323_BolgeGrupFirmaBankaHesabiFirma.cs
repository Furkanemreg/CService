using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CService.Core.Migrations
{
    /// <inheritdoc />
    public partial class BolgeGrupFirmaBankaHesabiFirma : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BankaHesaplari",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BankaAdi = table.Column<string>(type: "text", nullable: false),
                    SubeAdi = table.Column<string>(type: "text", nullable: true),
                    HesapNo = table.Column<string>(type: "text", nullable: true),
                    Iban = table.Column<string>(type: "text", nullable: true),
                    Aciklama = table.Column<string>(type: "text", nullable: true),
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
                    table.PrimaryKey("PK_BankaHesaplari", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Bolgeler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Ad = table.Column<string>(type: "text", nullable: false),
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
                    table.PrimaryKey("PK_Bolgeler", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GrupFirmalar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Ad = table.Column<string>(type: "text", nullable: false),
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
                    table.PrimaryKey("PK_GrupFirmalar", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Firmalar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirmaKodu = table.Column<string>(type: "text", nullable: false),
                    Adi = table.Column<string>(type: "text", nullable: false),
                    VergiNo = table.Column<string>(type: "text", nullable: true),
                    VergiDairesi = table.Column<string>(type: "text", nullable: true),
                    Tel1 = table.Column<string>(type: "text", nullable: true),
                    Not = table.Column<string>(type: "text", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    BolgeId = table.Column<int>(type: "integer", nullable: false),
                    GrupFirmaId = table.Column<int>(type: "integer", nullable: true),
                    VatRateId = table.Column<int>(type: "integer", nullable: false),
                    WitholdingRateId = table.Column<int>(type: "integer", nullable: true),
                    OkulServisi = table.Column<bool>(type: "boolean", nullable: false),
                    HavaleBankaHesabiId = table.Column<int>(type: "integer", nullable: true),
                    KrediKartiBankaHesabiId = table.Column<int>(type: "integer", nullable: true),
                    Unvan = table.Column<string>(type: "text", nullable: true),
                    Adres = table.Column<string>(type: "text", nullable: true),
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
                    table.PrimaryKey("PK_Firmalar", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Firmalar_BankaHesaplari_HavaleBankaHesabiId",
                        column: x => x.HavaleBankaHesabiId,
                        principalTable: "BankaHesaplari",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Firmalar_BankaHesaplari_KrediKartiBankaHesabiId",
                        column: x => x.KrediKartiBankaHesabiId,
                        principalTable: "BankaHesaplari",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Firmalar_Bolgeler_BolgeId",
                        column: x => x.BolgeId,
                        principalTable: "Bolgeler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Firmalar_GrupFirmalar_GrupFirmaId",
                        column: x => x.GrupFirmaId,
                        principalTable: "GrupFirmalar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Firmalar_VatRates_VatRateId",
                        column: x => x.VatRateId,
                        principalTable: "VatRates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Firmalar_WitholdingRates_WitholdingRateId",
                        column: x => x.WitholdingRateId,
                        principalTable: "WitholdingRates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Firmalar_BolgeId",
                table: "Firmalar",
                column: "BolgeId");

            migrationBuilder.CreateIndex(
                name: "IX_Firmalar_GrupFirmaId",
                table: "Firmalar",
                column: "GrupFirmaId");

            migrationBuilder.CreateIndex(
                name: "IX_Firmalar_HavaleBankaHesabiId",
                table: "Firmalar",
                column: "HavaleBankaHesabiId");

            migrationBuilder.CreateIndex(
                name: "IX_Firmalar_KrediKartiBankaHesabiId",
                table: "Firmalar",
                column: "KrediKartiBankaHesabiId");

            migrationBuilder.CreateIndex(
                name: "IX_Firmalar_VatRateId",
                table: "Firmalar",
                column: "VatRateId");

            migrationBuilder.CreateIndex(
                name: "IX_Firmalar_WitholdingRateId",
                table: "Firmalar",
                column: "WitholdingRateId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Firmalar");

            migrationBuilder.DropTable(
                name: "BankaHesaplari");

            migrationBuilder.DropTable(
                name: "Bolgeler");

            migrationBuilder.DropTable(
                name: "GrupFirmalar");
        }
    }
}
