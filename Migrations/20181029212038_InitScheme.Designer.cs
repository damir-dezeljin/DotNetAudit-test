﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using si.dezo.test.DotNetAudit.Persistence;

namespace si.dezo.test.DotNetAudit.Migrations
{
    [DbContext(typeof(TestDbContext))]
    [Migration("20181029212038_InitScheme")]
    partial class InitScheme
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("si.dezo.test.DotNetAudit.Models.Article", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedDt")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("Note")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(256)
                        .HasDefaultValue("");

                    b.Property<int>("PublicationId");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(64);

                    b.Property<int>("Type");

                    b.HasKey("Id");

                    b.HasIndex("PublicationId");

                    b.ToTable("Articles");
                });

            modelBuilder.Entity("si.dezo.test.DotNetAudit.Models.ArticleProposal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedDt")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("Note")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(256)
                        .HasDefaultValue("");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(64);

                    b.Property<int>("Type");

                    b.HasKey("Id");

                    b.ToTable("ArticleProposals");
                });

            modelBuilder.Entity("si.dezo.test.DotNetAudit.Models.Audit_Article", b =>
                {
                    b.Property<int>("Id");

                    b.Property<DateTime>("AuditDt")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("AuditAction")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue("");

                    b.Property<string>("AuditProcessAction")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(32)
                        .HasDefaultValue("<N/A>");

                    b.Property<string>("AuditTable")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(32)
                        .HasDefaultValue("");

                    b.Property<string>("Note")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(256)
                        .HasDefaultValue("");

                    b.Property<int>("PublicationId");

                    b.Property<string>("PublicationName")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue("");

                    b.Property<string>("Title")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(64)
                        .HasDefaultValue("");

                    b.Property<int>("Type");

                    b.Property<string>("TypeName")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue("");

                    b.HasKey("Id", "AuditDt");

                    b.ToTable("Audit_Articles");
                });

            modelBuilder.Entity("si.dezo.test.DotNetAudit.Models.Audit_Publication", b =>
                {
                    b.Property<int>("Id");

                    b.Property<DateTime>("AuditDt")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("ArticlesList")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue("");

                    b.Property<string>("AuditAction")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue("");

                    b.Property<string>("Name")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(100)
                        .HasDefaultValue("");

                    b.HasKey("Id", "AuditDt");

                    b.ToTable("Audit_Publications");
                });

            modelBuilder.Entity("si.dezo.test.DotNetAudit.Models.Publication", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedDt")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Publications");

                    b.HasData(
                        new { Id = 1, CreatedDt = new DateTime(2000, 1, 1, 1, 0, 0, 0, DateTimeKind.Local), Name = "Magazine A" },
                        new { Id = 2, CreatedDt = new DateTime(2000, 1, 1, 1, 0, 0, 0, DateTimeKind.Local), Name = "Blog 123" },
                        new { Id = 3, CreatedDt = new DateTime(2000, 1, 1, 1, 0, 0, 0, DateTimeKind.Local), Name = "Conference Proceeding" }
                    );
                });

            modelBuilder.Entity("si.dezo.test.DotNetAudit.Models.Article", b =>
                {
                    b.HasOne("si.dezo.test.DotNetAudit.Models.Publication", "Publication")
                        .WithMany("Articles")
                        .HasForeignKey("PublicationId")
                        .OnDelete(DeleteBehavior.Restrict);
                });
#pragma warning restore 612, 618
        }
    }
}
