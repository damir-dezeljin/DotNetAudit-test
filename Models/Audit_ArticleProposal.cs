using System;
using System.ComponentModel.DataAnnotations;
using Audit.EntityFramework;
using si.dezo.test.DotNetAudit.Misc;

namespace si.dezo.test.DotNetAudit.Models {
    public class Audit_ArticleProposal : IAudit {
        public int Id { get; set; }

        public ArticleType Type { get; set; }
        public string TypeName { get; set; }

        [StringLength (64)]
        public string Title { get; set; }

        [StringLength (256)]
        public string Note { get; set; }

        public int PublicationId { get; set; }
        public string PublicationName { get; set; }

        public DateTime AuditDt { get; set; }

        public string AuditAction { get; set; }

        [StringLength (32)]
        public string AuditTable { get; set; }

        [StringLength (32)]
        public string AuditProcessAction { get; set; }

        public Audit_ArticleProposal () { }
    }
}