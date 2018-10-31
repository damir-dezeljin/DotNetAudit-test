using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace si.dezo.test.DotNetAudit.Migrations
{
    public partial class Separate_Audit_ArticleProposals : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Audit_ArticleProposals",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Type = table.Column<int>(nullable: false),
                    TypeName = table.Column<string>(nullable: false, defaultValue: ""),
                    Title = table.Column<string>(maxLength: 64, nullable: false, defaultValue: ""),
                    Note = table.Column<string>(maxLength: 256, nullable: false, defaultValue: ""),
                    PublicationId = table.Column<int>(nullable: false),
                    PublicationName = table.Column<string>(nullable: false, defaultValue: ""),
                    AuditDt = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()"),
                    AuditAction = table.Column<string>(nullable: false, defaultValue: ""),
                    AuditTable = table.Column<string>(maxLength: 32, nullable: false, defaultValue: ""),
                    AuditProcessAction = table.Column<string>(maxLength: 32, nullable: false, defaultValue: "<N/A>")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Audit_ArticleProposals", x => new { x.Id, x.AuditDt });
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Audit_ArticleProposals");
        }
    }
}
