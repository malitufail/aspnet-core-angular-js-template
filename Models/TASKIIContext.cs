using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace taskiiApp.Models
{
    public partial class TASKIIContext : DbContext
    {
        public TASKIIContext()
        {
        }

        public TASKIIContext(DbContextOptions<TASKIIContext> options)
            : base(options)
        {}

        public virtual DbSet<TProjects> TProjects { get; set; }
        public virtual DbSet<TUsers> TUsers { get; set; }
        public virtual DbSet<TUserTasks> TUserTasks { get; set; }

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
//                optionsBuilder.UseSqlServer("Data Source=DESKTOP-IDUO46P\\SQLEXPRESS;Initial Catalog=TASKII;Persist Security Info=True;User ID=sa;Password=Wh0isthis??");
//            }
//        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TProjects>(entity =>
            {
                entity.ToTable("T_PROJECTS");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DateAdded)
                    .HasColumnName("DATE_ADDED")
                    .HasColumnType("datetime");

                entity.Property(e => e.DateUpdated)
                    .HasColumnName("DATE_UPDATED")
                    .HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .HasColumnName("DESCRIPTION")
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.ProjectName)
                    .HasColumnName("PROJECT_NAME")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.TotalHours).HasColumnName("TOTAL_HOURS");
            });

            modelBuilder.Entity<TUsers>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.ToTable("T_USERS");

                entity.Property(e => e.UserId).HasColumnName("USER_ID");

                entity.Property(e => e.AccessLevel)
                    .HasColumnName("ACCESS_LEVEL")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DateAdded)
                    .HasColumnName("DATE_ADDED")
                    .HasColumnType("datetime");

                entity.Property(e => e.DateUpdated)
                    .HasColumnName("DATE_UPDATED")
                    .HasColumnType("datetime");

                entity.Property(e => e.FirstName)
                    .HasColumnName("FIRST_NAME")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.IsActive)
                    .HasColumnName("IS_ACTIVE")
                    .HasMaxLength(10);

                entity.Property(e => e.LastLogin)
                    .HasColumnName("LAST_LOGIN")
                    .HasColumnType("datetime");

                entity.Property(e => e.LastName)
                    .HasColumnName("LAST_NAME")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasColumnName("PASSWORD")
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .HasColumnName("USERNAME")
                    .HasMaxLength(500)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TUserTasks>(entity =>
            {
                entity.ToTable("T_USER_TASKS");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DateAdded)
                    .HasColumnName("DATE_ADDED")
                    .HasColumnType("datetime");

                entity.Property(e => e.DateUpdated)
                    .HasColumnName("DATE_UPDATED")
                    .HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .HasColumnName("DESCRIPTION")
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Hours)
                    .HasColumnName("HOURS")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Project)
                    .HasColumnName("PROJECT")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.UserId).HasColumnName("USER_ID");
            });
        }
    }
}
