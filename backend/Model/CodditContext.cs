using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Backend.Model;

public partial class CodditContext : DbContext
{
    public CodditContext()
    {
    }

    public CodditContext(DbContextOptions<CodditContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<Forum> Forums { get; set; }

    public virtual DbSet<HasPermission> HasPermissions { get; set; }

    public virtual DbSet<Member> Members { get; set; }

    public virtual DbSet<Permission> Permissions { get; set; }

    public virtual DbSet<Post> Posts { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Vote> Votes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        EnvironmentVariable variable = new EnvironmentVariable(".env");
        var db = variable.Get<string>("DATABASE");
        optionsBuilder.UseSqlServer($"Data Source={db};Initial Catalog=Coddit;Integrated Security=True;TrustServerCertificate=true");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Comments__3214EC27F33A39E4");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CommentId).HasColumnName("CommentID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("date");
            entity.Property(e => e.Message)
                .IsRequired()
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.PostId).HasColumnName("PostID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.CommentNavigation).WithMany(p => p.InverseCommentNavigation)
                .HasForeignKey(d => d.CommentId)
                .HasConstraintName("FK__Comments__Commen__46E78A0C");

            entity.HasOne(d => d.Post).WithMany(p => p.Comments)
                .HasForeignKey(d => d.PostId)
                .HasConstraintName("FK__Comments__PostID__45F365D3");

            entity.HasOne(d => d.User).WithMany(p => p.Comments)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Comments__UserID__44FF419A");
        });

        modelBuilder.Entity<Forum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Forums__3214EC27A32350D0");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("date");
            entity.Property(e => e.Description)
                .IsRequired()
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<HasPermission>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__HasPermi__3214EC278A0C9974");

            entity.ToTable("HasPermission");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.PermissionId).HasColumnName("PermissionID");
            entity.Property(e => e.RoleId).HasColumnName("RoleID");

            entity.HasOne(d => d.Permission).WithMany(p => p.HasPermissions)
                .HasForeignKey(d => d.PermissionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__HasPermis__Permi__5812160E");

            entity.HasOne(d => d.Role).WithMany(p => p.HasPermissions)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__HasPermis__RoleI__571DF1D5");
        });

        modelBuilder.Entity<Member>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Members__3214EC2757CE55FC");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.ForumId).HasColumnName("ForumID");
            entity.Property(e => e.RoleId).HasColumnName("RoleID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Forum).WithMany(p => p.Members)
                .HasForeignKey(d => d.ForumId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Members__ForumID__5BE2A6F2");

            entity.HasOne(d => d.Role).WithMany(p => p.Members)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Members__RoleID__5CD6CB2B");

            entity.HasOne(d => d.User).WithMany(p => p.Members)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Members__UserID__5AEE82B9");
        });

        modelBuilder.Entity<Permission>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Permissi__3214EC27FE52C326");

            entity.HasIndex(e => e.Title, "UQ__Permissi__2CB664DC21A1B2E9").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Post>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Posts__3214EC278DAD719D");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("date");
            entity.Property(e => e.ForumId).HasColumnName("ForumID");
            entity.Property(e => e.Message)
                .IsRequired()
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Forum).WithMany(p => p.Posts)
                .HasForeignKey(d => d.ForumId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Posts__ForumID__412EB0B6");

            entity.HasOne(d => d.User).WithMany(p => p.Posts)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Posts__UserID__403A8C7D");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Roles__3214EC272B2E0274");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.ForumId).HasColumnName("ForumID");
            entity.Property(e => e.IsDefault).HasDefaultValueSql("((0))");
            entity.Property(e => e.IsOwner).HasDefaultValueSql("((0))");
            entity.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Forum).WithMany(p => p.Roles)
                .HasForeignKey(d => d.ForumId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Roles__ForumID__4F7CD00D");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC27B716CBB6");

            entity.HasIndex(e => e.Username, "UQ__Users__536C85E4B51C7BAE").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__Users__A9D10534A4FAA7F9").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.BirthDate).HasColumnType("date");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("date");
            entity.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.IsActive).HasDefaultValueSql("((0))");
            entity.Property(e => e.Password)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Salt)
                .IsRequired()
                .HasMaxLength(12)
                .IsUnicode(false);
            entity.Property(e => e.Username)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Vote>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Vote__3214EC27DF246883");

            entity.ToTable("Vote");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CommentId).HasColumnName("CommentID");
            entity.Property(e => e.PostId).HasColumnName("PostID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Comment).WithMany(p => p.Votes)
                .HasForeignKey(d => d.CommentId)
                .HasConstraintName("FK__Vote__CommentID__4CA06362");

            entity.HasOne(d => d.Post).WithMany(p => p.Votes)
                .HasForeignKey(d => d.PostId)
                .HasConstraintName("FK__Vote__PostID__4BAC3F29");

            entity.HasOne(d => d.User).WithMany(p => p.Votes)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Vote__UserID__4AB81AF0");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
