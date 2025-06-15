using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace GrpcServer.Models;

public partial class StackOverflow2010Context : DbContext
{
    public StackOverflow2010Context()
    {
    }

    public StackOverflow2010Context(DbContextOptions<StackOverflow2010Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Badge> Badges { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<LinkType> LinkTypes { get; set; }

    public virtual DbSet<Post> Posts { get; set; }

    public virtual DbSet<PostLink> PostLinks { get; set; }

    public virtual DbSet<PostType> PostTypes { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Vote> Votes { get; set; }

    public virtual DbSet<VoteType> VoteTypes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost,30081;Database=StackOverflow2010;User ID=sa;Password=Appie@123;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Badge>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Badges__Id");

            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(40);
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Comments__Id");

            entity.Property(e => e.CreationDate).HasColumnType("datetime");
            entity.Property(e => e.Text).HasMaxLength(700);
        });

        modelBuilder.Entity<LinkType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_LinkTypes__Id");

            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Post>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Posts__Id");

            entity.Property(e => e.ClosedDate).HasColumnType("datetime");
            entity.Property(e => e.CommunityOwnedDate).HasColumnType("datetime");
            entity.Property(e => e.CreationDate).HasColumnType("datetime");
            entity.Property(e => e.LastActivityDate).HasColumnType("datetime");
            entity.Property(e => e.LastEditDate).HasColumnType("datetime");
            entity.Property(e => e.LastEditorDisplayName).HasMaxLength(40);
            entity.Property(e => e.Tags).HasMaxLength(150);
            entity.Property(e => e.Title).HasMaxLength(250);
        });

        modelBuilder.Entity<PostLink>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_PostLinks__Id");

            entity.Property(e => e.CreationDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<PostType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_PostTypes__Id");

            entity.Property(e => e.Type).HasMaxLength(50);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Users_Id");

            entity.Property(e => e.CreationDate).HasColumnType("datetime");
            entity.Property(e => e.DisplayName).HasMaxLength(40);
            entity.Property(e => e.EmailHash).HasMaxLength(40);
            entity.Property(e => e.LastAccessDate).HasColumnType("datetime");
            entity.Property(e => e.Location).HasMaxLength(100);
            entity.Property(e => e.WebsiteUrl).HasMaxLength(200);
        });

        modelBuilder.Entity<Vote>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Votes__Id");

            entity.Property(e => e.CreationDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<VoteType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_VoteType__Id");

            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
