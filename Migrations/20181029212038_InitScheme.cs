using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace si.dezo.test.DotNetAudit.Migrations
{
    public partial class InitScheme : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ArticleProposals",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Type = table.Column<int>(nullable: false),
                    Title = table.Column<string>(maxLength: 64, nullable: false),
                    Note = table.Column<string>(maxLength: 256, nullable: false, defaultValue: ""),
                    CreatedDt = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleProposals", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Audit_Articles",
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
                    table.PrimaryKey("PK_Audit_Articles", x => new { x.Id, x.AuditDt });
                });

            migrationBuilder.CreateTable(
                name: "Audit_Publications",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: false, defaultValue: ""),
                    ArticlesList = table.Column<string>(nullable: false, defaultValue: ""),
                    AuditDt = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()"),
                    AuditAction = table.Column<string>(nullable: false, defaultValue: "")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Audit_Publications", x => new { x.Id, x.AuditDt });
                });

            migrationBuilder.CreateTable(
                name: "Publications",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    CreatedDt = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Publications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Articles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Type = table.Column<int>(nullable: false),
                    Title = table.Column<string>(maxLength: 64, nullable: false),
                    Note = table.Column<string>(maxLength: 256, nullable: false, defaultValue: ""),
                    PublicationId = table.Column<int>(nullable: false),
                    CreatedDt = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Articles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Articles_Publications_PublicationId",
                        column: x => x.PublicationId,
                        principalTable: "Publications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Publications",
                columns: new[] { "Id", "CreatedDt", "Name" },
                values: new object[] { 1, new DateTime(2000, 1, 1, 1, 0, 0, 0, DateTimeKind.Local), "Magazine A" });

            migrationBuilder.InsertData(
                table: "Publications",
                columns: new[] { "Id", "CreatedDt", "Name" },
                values: new object[] { 2, new DateTime(2000, 1, 1, 1, 0, 0, 0, DateTimeKind.Local), "Blog 123" });

            migrationBuilder.InsertData(
                table: "Publications",
                columns: new[] { "Id", "CreatedDt", "Name" },
                values: new object[] { 3, new DateTime(2000, 1, 1, 1, 0, 0, 0, DateTimeKind.Local), "Conference Proceeding" });

            migrationBuilder.CreateIndex(
                name: "IX_Articles_PublicationId",
                table: "Articles",
                column: "PublicationId");

            migrationBuilder.CreateIndex(
                name: "IX_Publications_Name",
                table: "Publications",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArticleProposals");

            migrationBuilder.DropTable(
                name: "Articles");

            migrationBuilder.DropTable(
                name: "Audit_Articles");

            migrationBuilder.DropTable(
                name: "Audit_Publications");

            migrationBuilder.DropTable(
                name: "Publications");
        }
    }
}
