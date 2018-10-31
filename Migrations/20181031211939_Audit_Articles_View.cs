using Microsoft.EntityFrameworkCore.Migrations;

namespace si.dezo.test.DotNetAudit.Migrations {
    public partial class Audit_Articles_View : Migration {
        protected override void Up (MigrationBuilder migrationBuilder) {
            migrationBuilder.Sql (@"
CREATE VIEW Audit_Articles_View AS
  SELECT * FROM Audit_Articles
  UNION ALL
  SELECT * FROM Audit_ArticleProposals
");
        }

        protected override void Down (MigrationBuilder migrationBuilder) {
            migrationBuilder.Sql (@"DROP VIEW Audit_Articles_View");
        }
    }
}