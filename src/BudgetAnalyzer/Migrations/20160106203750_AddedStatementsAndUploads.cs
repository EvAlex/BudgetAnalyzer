using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;

namespace BudgetAnalyzer.Migrations
{
    public partial class AddedStatementsAndUploads : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FileUploads",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:Serial", true),
                    Content = table.Column<byte[]>(nullable: true),
                    ContentType = table.Column<string>(nullable: true),
                    FileName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileUpload", x => x.Id);
                });
            migrationBuilder.CreateTable(
                name: "AccountStatements",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:Serial", true),
                    BankAccountId = table.Column<int>(nullable: false),
                    FileUploadId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountStatement", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccountStatement_BankAccount_BankAccountId",
                        column: x => x.BankAccountId,
                        principalTable: "BankAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccountStatement_FileUpload_FileUploadId",
                        column: x => x.FileUploadId,
                        principalTable: "FileUploads",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
            migrationBuilder.AddColumn<int>(
                name: "StatementId",
                table: "AccountOperations",
                nullable: true);
            migrationBuilder.AddForeignKey(
                name: "FK_AccountOperation_AccountStatement_StatementId",
                table: "AccountOperations",
                column: "StatementId",
                principalTable: "AccountStatements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "FK_AccountOperation_AccountStatement_StatementId", table: "AccountOperations");
            migrationBuilder.DropColumn(name: "StatementId", table: "AccountOperations");
            migrationBuilder.DropTable("AccountStatements");
            migrationBuilder.DropTable("FileUploads");
        }
    }
}
