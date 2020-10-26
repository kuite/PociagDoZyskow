using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PociagDoZyskow.DataAccess.Migrations.ExternalDataReads
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "ExternalDataScans");

            migrationBuilder.CreateTable(
                name: "CompanyDataScans",
                schema: "ExternalDataScans",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ScanReferenceTime = table.Column<DateTime>(nullable: false),
                    ReferencePrice = table.Column<decimal>(nullable: false),
                    OpenPrice = table.Column<decimal>(nullable: false),
                    LowestPrice = table.Column<decimal>(nullable: false),
                    HighestPrice = table.Column<decimal>(nullable: false),
                    LastPrice = table.Column<decimal>(nullable: false),
                    ChangePrice = table.Column<decimal>(nullable: false),
                    TotalTransactionVolumeStockCount = table.Column<int>(nullable: false),
                    TotalTransactionValue = table.Column<decimal>(nullable: false),
                    TransactionsCount = table.Column<int>(nullable: false),
                    CompanyId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyDataScans", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FinancialReportTimeDataScans",
                schema: "ExternalDataScans",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyId = table.Column<int>(nullable: false),
                    CompanyTicker = table.Column<string>(nullable: true),
                    ShortCompanyName = table.Column<string>(nullable: true),
                    FullCompanyName = table.Column<string>(nullable: true),
                    ReportType = table.Column<string>(nullable: true),
                    ReportDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinancialReportTimeDataScans", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompanyDataScans",
                schema: "ExternalDataScans");

            migrationBuilder.DropTable(
                name: "FinancialReportTimeDataScans",
                schema: "ExternalDataScans");
        }
    }
}
