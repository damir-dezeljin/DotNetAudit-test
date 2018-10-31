using System;
using System.ComponentModel.DataAnnotations;
using Audit.EntityFramework;
using si.dezo.test.DotNetAudit.Misc;

namespace si.dezo.test.DotNetAudit.Models {
    public class Audit_Article_View  {
        public int Id { get; private set; }

        public ArticleType Type { get; private set; }
        public string TypeName { get; private set; }

        public string Title { get; private set; }

        public string Note { get; private set; }

        public int PublicationId { get; private set; }
        public string PublicationName { get; private set; }

        public DateTime AuditDt { get; private set; }

        public string AuditAction { get; private set; }

        public string AuditTable { get; private set; }

        public string AuditProcessAction { get; private set; }

        public Audit_Article_View () { }
    }
}