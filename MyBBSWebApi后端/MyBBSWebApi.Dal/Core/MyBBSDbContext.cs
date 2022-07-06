using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using MyBBSWebApi.Models.Models;

#nullable disable

namespace MyBBSWebApi.Dal.Core
{
    public partial class MyBBSDbContext : DbContext
    {
        public MyBBSDbContext()
        {
        }

        public MyBBSDbContext(DbContextOptions<MyBBSDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Post> Posts { get; set; }//Post是实体类Post；Posts是context起的属性名
        public virtual DbSet<PostReply> PostReplys { get; set; }
        public virtual DbSet<PostType> PostTypes { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("server=.;uid=sa;pwd=123456;database=MyBBSDb;TrustServerCertificate=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Chinese_PRC_CI_AS");

            modelBuilder.Entity<Post>(entity =>
            {
                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.Down).HasColumnType("text");

                entity.Property(e => e.EditTime).HasColumnType("datetime");

                entity.Property(e => e.LastReplyTime).HasColumnType("datetime");

                entity.Property(e => e.PostContent).HasColumnType("text");

                entity.Property(e => e.PostIcon).HasMaxLength(500);

                entity.Property(e => e.PostTitle).HasMaxLength(100);

                entity.Property(e => e.PostType)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Up).HasColumnType("text");
            });

            modelBuilder.Entity<PostReply>(entity =>
            {
                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.Down).HasColumnType("text");

                entity.Property(e => e.EditTime).HasColumnType("datetime");

                entity.Property(e => e.ReplyContent).HasColumnType("text");

                entity.Property(e => e.Up).HasColumnType("text");
            });

            modelBuilder.Entity<PostType>(entity =>
            {
                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.PostType1)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("PostType");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.AutoLoginLimitTime).HasColumnType("datetime");

                entity.Property(e => e.Password).HasMaxLength(50);

                entity.Property(e => e.UserName).HasMaxLength(50);

                entity.Property(e => e.UserNo)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
