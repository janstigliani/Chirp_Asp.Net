using System;
using System.Collections.Generic;
using Chirp.Model;
using Microsoft.EntityFrameworkCore;

namespace Chirp;

public partial class ChirpContext : DbContext
{
    public ChirpContext()
    {
    }

    public ChirpContext(DbContextOptions<ChirpContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Chirps> Chirps { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=Chirp;Username=postgres;Password=superpippo;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Chirps>(entity =>
        {
            entity.HasKey(e => e.ChirpsId).HasName("Chirps_pkey");

            entity.Property(e => e.ChirpsId).HasColumnName("Chirps_id");
            entity.Property(e => e.CreationTime)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("Creation_Time");
            entity.Property(e => e.ExternalUrl)
                .HasMaxLength(2083)
                .HasColumnName("External_Url");
            entity.Property(e => e.Text).HasMaxLength(140);
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.CommentId).HasName("Comments_pkey");

            entity.Property(e => e.CommentId).HasColumnName("Comment_Id");
            entity.Property(e => e.ChirpId).HasColumnName("Chirp_Id");
            entity.Property(e => e.CreationTime)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("Creation_Time");
            entity.Property(e => e.ParentId).HasColumnName("Parent_Id");
            entity.Property(e => e.Text).HasMaxLength(140);

            entity.HasOne(d => d.Chirp).WithMany(p => p.Comments)
                .HasForeignKey(d => d.ChirpId)
                .HasConstraintName("Chirp_Id_Fk");

            entity.HasOne(d => d.Parent).WithMany(p => p.InverseParent)
                .HasForeignKey(d => d.ParentId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Comment_Id_Fk");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
