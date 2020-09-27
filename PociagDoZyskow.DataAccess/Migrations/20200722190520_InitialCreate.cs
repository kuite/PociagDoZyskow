using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PociagDoZyskow.DataAccess.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "stock");

            migrationBuilder.CreateTable(
                name: "Exchange",
                schema: "stock",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    ShortName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exchange", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Company",
                schema: "stock",
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
                    table.PrimaryKey("PK_Company", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Company_Exchange_ExchangeId",
                        column: x => x.ExchangeId,
                        principalSchema: "stock",
                        principalTable: "Exchange",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Record",
                schema: "stock",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LastTransactionTime = table.Column<DateTime>(nullable: false),
                    ReferencePrice = table.Column<decimal>(nullable: false),
                    OpenPrice = table.Column<decimal>(nullable: false),
                    LowestPrice = table.Column<decimal>(nullable: false),
                    HighestPrice = table.Column<decimal>(nullable: false),
                    LastPrice = table.Column<decimal>(nullable: false),
                    LastTransactionVolume = table.Column<int>(nullable: false),
                    TotalTransactionVolume = table.Column<int>(nullable: false),
                    TransactionsCount = table.Column<int>(nullable: false),
                    CompanyId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Record", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Record_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalSchema: "stock",
                        principalTable: "Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Company_ExchangeId",
                schema: "stock",
                table: "Company",
                column: "ExchangeId");

            migrationBuilder.CreateIndex(
                name: "IX_Record_CompanyId",
                schema: "stock",
                table: "Record",
                column: "CompanyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Record",
                schema: "stock");

            migrationBuilder.DropTable(
                name: "Company",
                schema: "stock");

            migrationBuilder.DropTable(
                name: "Exchange",
                schema: "stock");
        }
    }
}
