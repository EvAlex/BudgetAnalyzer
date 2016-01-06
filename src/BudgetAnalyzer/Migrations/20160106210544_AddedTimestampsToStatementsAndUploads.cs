using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;

namespace BudgetAnalyzer.Migrations
{
    public partial class AddedTimestampsToStatementsAndUploads : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreatedAt",
                table: "FileUploads",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));
            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "ProcessedAt",
                table: "AccountStatements",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "CreatedAt", table: "FileUploads");
            migrationBuilder.DropColumn(name: "ProcessedAt", table: "AccountStatements");
        }
    }
}
