using System;
using System.ComponentModel.DataAnnotations;
using Audit.EntityFramework;
using si.dezo.test.DotNetAudit.Misc;

namespace si.dezo.test.DotNetAudit.Models {
    [AuditInclude]
    public class ArticleProposal {
        public int Id { get; set; }

        [Required]
        public ArticleType Type { get; set; }

        [Required]
        [StringLength (64)]
        public string Title { get; set; }

        [StringLength (256)]
        public string Note { get; set; }

        public DateTime CreatedDt { get; set; }

        public ArticleProposal () { }

        public ArticleProposal (
            int id,
            ArticleType type,
            string title,
            string note,
            DateTime? createdDt = null
        ) : this () {
            this.Id = id;
            this.Type = type;
            this.Title = title;
            this.Note = note;
            if (createdDt == null)
                this.CreatedDt = DateTime.Parse ("2000-01-01 00:00:00Z");
            else
                this.CreatedDt = (DateTime) createdDt;

        }

    }
}