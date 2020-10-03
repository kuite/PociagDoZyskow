﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PociagDoZyskow.DataAccess.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StockExchanges",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    ShortName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockExchanges", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    ShortName = table.Column<string>(nullable: true),
                    ExchangeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Companies_StockExchanges_ExchangeId",
                        column: x => x.ExchangeId,
                        principalTable: "StockExchanges",
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
                name: "FinancialReportTimeDataScans",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ticker = table.Column<string>(nullable: true),
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

            migrationBuilder.CreateTable(
                name: "Records",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ScanTime = table.Column<DateTime>(nullable: false),
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
                    table.PrimaryKey("PK_Records", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Records_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "StockExchanges",
                columns: new[] { "Id", "Name", "ShortName" },
                values: new object[] { 1, "NewConnect", "NC" });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "ExchangeId", "Name", "ShortName" },
                values: new object[] { 1, 1, "Cookieland", "CL" });

            migrationBuilder.InsertData(
                table: "Records",
                columns: new[] { "Id", "ChangePrice", "CompanyId", "HighestPrice", "LastPrice", "LowestPrice", "OpenPrice", "ReferencePrice", "ScanTime", "TotalTransactionValue", "TotalTransactionVolumeStockCount", "TransactionsCount" },
                values: new object[] { 1, 0m, 1, 1.0m, 1.0m, 1.0m, 1.0m, 1.0m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0m, 1, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_AlgorithmResults_CompanyId",
                table: "AlgorithmResults",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_ExchangeId",
                table: "Companies",
                column: "ExchangeId");

            migrationBuilder.CreateIndex(
                name: "IX_FinancialReportTimeDataScans_CompanyId",
                table: "FinancialReportTimeDataScans",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Records_CompanyId",
                table: "Records",
                column: "CompanyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AlgorithmResults");

            migrationBuilder.DropTable(
                name: "FinancialReportTimeDataScans");

            migrationBuilder.DropTable(
                name: "Records");

            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropTable(
                name: "StockExchanges");
        }
    }
}
