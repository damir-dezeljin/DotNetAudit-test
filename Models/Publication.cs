using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Audit.EntityFramework;
using si.dezo.test.DotNetAudit.Misc;

namespace si.dezo.test.DotNetAudit.Models {
    [AuditInclude]
    public class Publication {
        public int Id { get; set; }

        [Required]
        [StringLength (100)]
        public string Name { get; set; }

        [InverseProperty ("Publication")]
        public ICollection<Article> Articles { get; set; }

        public DateTime CreatedDt { get; set; }
        public Publication () {
            Articles = new Collection<Article> ();
        }

        public Publication (
            int id,
            string name,
            DateTime? createdDt = null
        ) : this () {
            this.Id = id;
            this.Name = name;
            if (createdDt == null)
                this.CreatedDt = DateTime.Parse ("2000-01-01 00:00:00Z");
            else
                this.CreatedDt = (DateTime) createdDt;
        }

    }
}