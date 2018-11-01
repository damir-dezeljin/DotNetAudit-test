using System;
using Audit.Core;
using Audit.EntityFramework;
using Microsoft.EntityFrameworkCore;
using si.dezo.test.DotNetAudit.Models;

namespace si.dezo.test.DotNetAudit.Persistence {
    /* DB creation script
-- drop database
ALTER DATABASE [audit_test] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
DROP DATABASE audit_test
GO

-- create database
CREATE DATABASE audit_test
GO

-- drop user and create it again
DROP LOGIN [test_user]
GO
CREATE LOGIN [test_user] WITH PASSWORD='TestPassword1234'
GO

-- grant permissions on DB
USE [audit_test]
GO
DROP USER [test_user]
GO
CREATE USER [test_user] FOR LOGIN [test_user] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [test_user]
GO

-- help comments (optional / use them only if you understand what they do)
-- EXEC sp_addrolemember [db_owner], [test_user]
-- ALTER USER [test_user] WITH DEFAULT_SCHEMA = [dbo];
     */
    /// <summary>
    /// Application DB context
    /// </summary>
    /// <note type="tip">
    /// For DB and DB user creation script, check the comment in the <see cref="TestDbContext"/> class
    /// </note>
    [AuditDbContext (Mode = AuditOptionMode.OptIn, IncludeEntityObjects = false)]
    public class TestDbContext : AuditDbContext {
        public DbSet<Article> Articles { get; set; }
        public DbSet<ArticleProposal> ArticleProposals { get; set; }
        public DbSet<Publication> Publications { get; set; }
        public DbSet<Audit_Article> Audit_Articles { get; set; }
        public DbSet<Audit_Publication> Audit_Publications { get; set; }

        public TestDbContext (DbContextOptions<TestDbContext> options) : base (options) { }

        protected override void OnModelCreating (ModelBuilder modelBuilder) {
            // modelBuilder.HasDefaultSchema("audit_test");

            Audit.Core.Configuration.Setup ()
                .UseEntityFramework (_ => _
                    .AuditTypeExplicitMapper (m => m
                        .Map<Publication, Audit_Publication> ()
                        .Map<Article, Audit_Article> ((item, auditTbl) => {
                            auditTbl.TypeName = item.Type.ToString ();
                            auditTbl.PublicationName = item.Publication?.Name;
                        })
                        .Map<ArticleProposal, Audit_Article> ((item, auditTbl) => {
                            auditTbl.TypeName = item.Type.ToString ();
                            auditTbl.PublicationName = "<N/A>";
                        })
                        .AuditEntityAction<IAudit> ((evt, entry, auditTbl) => {
                            auditTbl.AuditDt = DateTime.UtcNow;
                            auditTbl.AuditAction = entry.Action;

                            if (auditTbl is Audit_Article) {
                                var auditArticle = auditTbl as Audit_Article;
                                auditArticle.AuditTable = entry.Table;

                                bool bRv = false;
                                dynamic val;
                                bRv = evt.CustomFields.TryGetValue ("AuditProcessAction", out val);
                                string processAction = (bRv && val is string) ? val as string : "<UNKNOWN>";
                                auditArticle.AuditProcessAction = processAction;
                            }
                        })
                    )
                );

            modelBuilder.Entity<Article> ()
                .Property (d => d.Note)
                .IsRequired (true)
                .HasDefaultValue ("");
            modelBuilder.Entity<Article> ()
                .Property (d => d.CreatedDt)
                .IsRequired (true)
                .HasDefaultValueSql ("GETDATE()");
            modelBuilder.Entity<Article> ()
                .HasOne (d => d.Publication)
                .WithMany (r => r.Articles)
                .OnDelete (DeleteBehavior.Restrict);

            modelBuilder.Entity<ArticleProposal> ()
                .Property (d => d.Note)
                .IsRequired (true)
                .HasDefaultValue ("");
            modelBuilder.Entity<ArticleProposal> ()
                .Property (d => d.CreatedDt)
                .IsRequired (true)
                .HasDefaultValueSql ("GETDATE()");

            modelBuilder.Entity<Publication> ()
                .HasIndex (d => d.Name).IsUnique ();
            modelBuilder.Entity<Publication> ()
                .Property (d => d.CreatedDt)
                .IsRequired (true)
                .HasDefaultValueSql ("GETDATE()");
            modelBuilder.Entity<Publication> ().HasData (
                new Publication (1, "Magazine A"),
                new Publication (2, "Blog 123"),
                new Publication (3, "Conference Proceeding")
            );

            modelBuilder.Entity<Audit_Article> ().HasKey (
                d => new { d.Id, d.AuditDt });
            modelBuilder.Entity<Audit_Article> ()
                .Property (d => d.TypeName)
                .IsRequired (true)
                .HasDefaultValue ("");
            modelBuilder.Entity<Audit_Article> ()
                .Property (d => d.Title)
                .IsRequired (true)
                .HasDefaultValue ("");
            modelBuilder.Entity<Audit_Article> ()
                .Property (d => d.Note)
                .IsRequired (true)
                .HasDefaultValue ("");
            modelBuilder.Entity<Audit_Article> ()
                .Property (d => d.PublicationName)
                .IsRequired (true)
                .HasDefaultValue ("");
            modelBuilder.Entity<Audit_Article> ()
                .Property (d => d.AuditAction)
                .IsRequired (true)
                .HasDefaultValue ("");
            modelBuilder.Entity<Audit_Article> ()
                .Property (d => d.AuditTable)
                .IsRequired (true)
                .HasDefaultValue ("");
            modelBuilder.Entity<Audit_Article> ()
                .Property (d => d.AuditProcessAction)
                .IsRequired (true)
                .HasDefaultValue ("<N/A>");
            modelBuilder.Entity<Audit_Article> ()
                .Property (d => d.AuditDt)
                .IsRequired (true)
                .HasDefaultValueSql ("GETDATE()");

            modelBuilder.Entity<Audit_Publication> ().HasKey (
                d => new { d.Id, d.AuditDt });
            modelBuilder.Entity<Audit_Publication> ()
                .Property (d => d.Name)
                .IsRequired (true)
                .HasDefaultValue ("");
            modelBuilder.Entity<Audit_Publication> ()
                .Property (d => d.ArticlesList)
                .IsRequired (true)
                .HasDefaultValue ("");
            modelBuilder.Entity<Audit_Publication> ()
                .Property (d => d.AuditAction)
                .IsRequired (true)
                .HasDefaultValue ("");
            modelBuilder.Entity<Audit_Publication> ()
                .Property (d => d.AuditDt)
                .IsRequired (true)
                .HasDefaultValueSql ("GETDATE()");
        }

    }
}