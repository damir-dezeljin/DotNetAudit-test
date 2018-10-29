using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Audit.EntityFramework;
using si.dezo.test.DotNetAudit.Misc;

namespace si.dezo.test.DotNetAudit.Models {
    public class Audit_Publication : IAudit {
        [DatabaseGenerated (DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [StringLength (100)]
        public string Name { get; set; }

        public string ArticlesList { get; set; }

        public DateTime AuditDt { get; set; }

        public string AuditAction { get; set; }
    }
}