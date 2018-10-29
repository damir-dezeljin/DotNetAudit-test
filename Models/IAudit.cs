using System;
using System.ComponentModel.DataAnnotations;
using Audit.EntityFramework;
using si.dezo.test.DotNetAudit.Misc;

namespace si.dezo.test.DotNetAudit.Models {
    public interface IAudit {
        DateTime AuditDt { get; set; }
        string AuditAction { get; set; }
    }
}