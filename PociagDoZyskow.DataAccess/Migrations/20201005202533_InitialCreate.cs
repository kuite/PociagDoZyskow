using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PociagDoZyskow.DataAccess.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Exchanges",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    ShortName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exchanges", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ticker = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    ShortName = table.Column<string>(nullable: true),
                    ExchangeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Companies_Exchanges_ExchangeId",
                        column: x => x.ExchangeId,
                        principalTable: "Exchanges",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AlgorithmResults",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyId = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    AlgorithmName = table.Column<string>(nullable: true),
                    IsBuy = table.Column<bool>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlgorithmResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AlgorithmResults_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CompanyDataScans",
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
                    table.ForeignKey(
                        name: "FK_CompanyDataScans_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FinancialReportTimeDataScans",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyTicker = table.Column<string>(nullable: true),
                    CompanyId = table.Column<int>(nullable: false),
                    ShortCompanyName = table.Column<string>(nullable: true),
                    FullCompanyName = table.Column<string>(nullable: true),
                    ReportType = table.Column<string>(nullable: true),
                    ReportDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinancialReportTimeDataScans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FinancialReportTimeDataScans_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AlgorithmResults_CompanyId",
                table: "AlgorithmResults",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_ExchangeId",
                table: "Companies",
                column: "ExchangeId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyDataScans_CompanyId",
                table: "CompanyDataScans",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_FinancialReportTimeDataScans_CompanyId",
                table: "FinancialReportTimeDataScans",
                column: "CompanyId");

            migrationBuilder.InsertData(
                table: "Exchanges",
                columns: new[] { "Id", "Name", "ShortName" },
                values: new object[,]
                {
                    { 1, "NewConnect", "NC" },
                    { 2, "Gielda Papierow Wartosciowych", "GPW" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AlgorithmResults");

            migrationBuilder.DropTable(
                name: "CompanyDataScans");

            migrationBuilder.DropTable(
                name: "FinancialReportTimeDataScans");

            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropTable(
                name: "Exchanges");
        }
    }
}
