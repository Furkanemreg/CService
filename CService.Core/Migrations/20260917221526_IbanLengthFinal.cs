using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CService.Core.Migrations
{
    /// <inheritdoc />
    public partial class IbanLengthFinal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Iban",
                table: "BankaHesaplari",
                type: "character varying(26)",
                maxLength: 26,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Iban",
                table: "BankaHesaplari",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(26)",
                oldMaxLength: 26,
                oldNullable: true);
        }
    }
}
