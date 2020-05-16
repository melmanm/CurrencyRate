using Microsoft.EntityFrameworkCore.Migrations;

namespace CurrencyRate.Infrastructure.Migrations
{
    public partial class NewProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsFromPreviousDay",
                table: "CurrencyExchangeRate",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsFromPreviousDay",
                table: "CurrencyExchangeRate");
        }
    }
}
