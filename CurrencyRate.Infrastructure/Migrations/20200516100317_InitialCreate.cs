using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CurrencyRate.Infrastructure.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApiKey",
                columns: table => new
                {
                    Key = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApiKey", x => x.Key);
                });

            migrationBuilder.CreateTable(
                name: "CurrencyExchangeRate",
                columns: table => new
                {
                    CurrencyFrom = table.Column<string>(nullable: false),
                    CurrrencyTo = table.Column<string>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    Value = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurrencyExchangeRate", x => new { x.CurrencyFrom, x.CurrrencyTo, x.Date });
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApiKey");

            migrationBuilder.DropTable(
                name: "CurrencyExchangeRate");
        }
    }
}
