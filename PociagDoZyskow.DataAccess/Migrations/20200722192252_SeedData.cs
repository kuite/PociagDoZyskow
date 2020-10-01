using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PociagDoZyskow.DataAccess.Migrations
{
    public partial class SeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "stock",
                table: "Exchange",
                columns: new[] { "Id", "Name", "ShortName" },
                values: new object[] { 1, "NewConnect", "NC" });

            migrationBuilder.InsertData(
                schema: "stock",
                table: "Company",
                columns: new[] { "Id", "ExchangeId", "Name", "ShortName" },
                values: new object[] { 1, 1, "Cookieland", "CL" });

            migrationBuilder.InsertData(
                schema: "stock",
                table: "Record",
                columns: new[] { "Id", "CompanyId", "HighestPrice", "LastPrice", "LastTransactionTime", "LastTransactionVolume", "LowestPrice", "OpenPrice", "ReferencePrice", "TotalTransactionVolumeStockCount", "TransactionsCount" },
                values: new object[] { 1, 1, 1.0m, 1.0m, new DateTime(2020, 7, 22, 19, 22, 52, 281, DateTimeKind.Utc).AddTicks(9923), 1, 1.0m, 1.0m, 1.0m, 1, 1 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "stock",
                table: "Record",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                schema: "stock",
                table: "Company",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                schema: "stock",
                table: "Exchange",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
