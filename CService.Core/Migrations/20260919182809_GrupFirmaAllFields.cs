using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CService.Core.Migrations
{
    /// <inheritdoc />
    public partial class GrupFirmaAllFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Ad",
                table: "GrupFirmalar",
                newName: "FirmaKodu");

            migrationBuilder.AddColumn<string>(
                name: "Adi",
                table: "GrupFirmalar",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Adres",
                table: "GrupFirmalar",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BolgeId",
                table: "GrupFirmalar",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HavaleBankaHesabiId",
                table: "GrupFirmalar",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "GrupFirmalar",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "KrediKartiBankaHesabiId",
                table: "GrupFirmalar",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Mail",
                table: "GrupFirmalar",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Not",
                table: "GrupFirmalar",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "OkulServisi",
                table: "GrupFirmalar",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Tel1",
                table: "GrupFirmalar",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Tel2",
                table: "GrupFirmalar",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Unvan",
                table: "GrupFirmalar",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VatRateId",
                table: "GrupFirmalar",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "VergiDairesi",
                table: "GrupFirmalar",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VergiNo",
                table: "GrupFirmalar",
                type: "character varying(11)",
                maxLength: 11,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WitholdingRateId",
                table: "GrupFirmalar",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_GrupFirmalar_BolgeId",
                table: "GrupFirmalar",
                column: "BolgeId");

            migrationBuilder.CreateIndex(
                name: "IX_GrupFirmalar_HavaleBankaHesabiId",
                table: "GrupFirmalar",
                column: "HavaleBankaHesabiId");

            migrationBuilder.CreateIndex(
                name: "IX_GrupFirmalar_KrediKartiBankaHesabiId",
                table: "GrupFirmalar",
                column: "KrediKartiBankaHesabiId");

            migrationBuilder.CreateIndex(
                name: "IX_GrupFirmalar_VatRateId",
                table: "GrupFirmalar",
                column: "VatRateId");

            migrationBuilder.CreateIndex(
                name: "IX_GrupFirmalar_WitholdingRateId",
                table: "GrupFirmalar",
                column: "WitholdingRateId");

            migrationBuilder.AddForeignKey(
                name: "FK_GrupFirmalar_BankaHesaplari_HavaleBankaHesabiId",
                table: "GrupFirmalar",
                column: "HavaleBankaHesabiId",
                principalTable: "BankaHesaplari",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GrupFirmalar_BankaHesaplari_KrediKartiBankaHesabiId",
                table: "GrupFirmalar",
                column: "KrediKartiBankaHesabiId",
                principalTable: "BankaHesaplari",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GrupFirmalar_Bolgeler_BolgeId",
                table: "GrupFirmalar",
                column: "BolgeId",
                principalTable: "Bolgeler",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GrupFirmalar_VatRates_VatRateId",
                table: "GrupFirmalar",
                column: "VatRateId",
                principalTable: "VatRates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GrupFirmalar_WitholdingRates_WitholdingRateId",
                table: "GrupFirmalar",
                column: "WitholdingRateId",
                principalTable: "WitholdingRates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GrupFirmalar_BankaHesaplari_HavaleBankaHesabiId",
                table: "GrupFirmalar");

            migrationBuilder.DropForeignKey(
                name: "FK_GrupFirmalar_BankaHesaplari_KrediKartiBankaHesabiId",
                table: "GrupFirmalar");

            migrationBuilder.DropForeignKey(
                name: "FK_GrupFirmalar_Bolgeler_BolgeId",
                table: "GrupFirmalar");

            migrationBuilder.DropForeignKey(
                name: "FK_GrupFirmalar_VatRates_VatRateId",
                table: "GrupFirmalar");

            migrationBuilder.DropForeignKey(
                name: "FK_GrupFirmalar_WitholdingRates_WitholdingRateId",
                table: "GrupFirmalar");

            migrationBuilder.DropIndex(
                name: "IX_GrupFirmalar_BolgeId",
                table: "GrupFirmalar");

            migrationBuilder.DropIndex(
                name: "IX_GrupFirmalar_HavaleBankaHesabiId",
                table: "GrupFirmalar");

            migrationBuilder.DropIndex(
                name: "IX_GrupFirmalar_KrediKartiBankaHesabiId",
                table: "GrupFirmalar");

            migrationBuilder.DropIndex(
                name: "IX_GrupFirmalar_VatRateId",
                table: "GrupFirmalar");

            migrationBuilder.DropIndex(
                name: "IX_GrupFirmalar_WitholdingRateId",
                table: "GrupFirmalar");

            migrationBuilder.DropColumn(
                name: "Adi",
                table: "GrupFirmalar");

            migrationBuilder.DropColumn(
                name: "Adres",
                table: "GrupFirmalar");

            migrationBuilder.DropColumn(
                name: "BolgeId",
                table: "GrupFirmalar");

            migrationBuilder.DropColumn(
                name: "HavaleBankaHesabiId",
                table: "GrupFirmalar");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "GrupFirmalar");

            migrationBuilder.DropColumn(
                name: "KrediKartiBankaHesabiId",
                table: "GrupFirmalar");

            migrationBuilder.DropColumn(
                name: "Mail",
                table: "GrupFirmalar");

            migrationBuilder.DropColumn(
                name: "Not",
                table: "GrupFirmalar");

            migrationBuilder.DropColumn(
                name: "OkulServisi",
                table: "GrupFirmalar");

            migrationBuilder.DropColumn(
                name: "Tel1",
                table: "GrupFirmalar");

            migrationBuilder.DropColumn(
                name: "Tel2",
                table: "GrupFirmalar");

            migrationBuilder.DropColumn(
                name: "Unvan",
                table: "GrupFirmalar");

            migrationBuilder.DropColumn(
                name: "VatRateId",
                table: "GrupFirmalar");

            migrationBuilder.DropColumn(
                name: "VergiDairesi",
                table: "GrupFirmalar");

            migrationBuilder.DropColumn(
                name: "VergiNo",
                table: "GrupFirmalar");

            migrationBuilder.DropColumn(
                name: "WitholdingRateId",
                table: "GrupFirmalar");

            migrationBuilder.RenameColumn(
                name: "FirmaKodu",
                table: "GrupFirmalar",
                newName: "Ad");
        }
    }
}
