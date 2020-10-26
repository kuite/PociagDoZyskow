using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PociagDoZyskow.DataAccess.Migrations.Algorithm
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Algorithms");

            migrationBuilder.CreateTable(
                name: "AlgorithmResults",
                schema: "Algorithms",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyId = table.Column<int>(nullable: false),
                    ResultDescription = table.Column<string>(nullable: true),
                    AlgorithmName = table.Column<string>(nullable: true),
                    IsBuy = table.Column<bool>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlgorithmResults", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AlgorithmResults",
                schema: "Algorithms");
        }
    }
}
