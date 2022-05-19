using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Online_Test_Platform.Models
{
    public partial class TestPlatformContext : DbContext
    {
        public TestPlatformContext()
        {
        }

        public TestPlatformContext(DbContextOptions<TestPlatformContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Question> Questions { get; set; } = null!;
        public virtual DbSet<TestCatagory> TestCatagories { get; set; } = null!;
        public virtual DbSet<TestReport> TestReports { get; set; } = null!;
        public virtual DbSet<UserAnswer> UserAnswers { get; set; } = null!;
        public virtual DbSet<UserInfo> UserInfos { get; set; } = null!;
        public virtual DbSet<UserRole> UserRoles { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#pragma warning disable CS1030 // #warning directive
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=TestPlatform;Integrated Security=SSPI");
#pragma warning restore CS1030 // #warning directive
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Question>(entity =>
            {
                entity.Property(e => e.QuestionId).HasColumnName("QuestionID");

                entity.Property(e => e.CorrectAnswer)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Option1)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Option2)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Option3)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Option4)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Question1)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("Question");

                entity.Property(e => e.TestCatagoryId).HasColumnName("TestCatagoryID");

                entity.HasOne(d => d.TestCatagory)
                    .WithMany(p => p.Questions)
                    .HasForeignKey(d => d.TestCatagoryId)
                    .HasConstraintName("FK__Questions__TestC__4CA06362");
            });

            modelBuilder.Entity<TestCatagory>(entity =>
            {
                entity.ToTable("TestCatagory");

                entity.Property(e => e.TestCatagoryId).HasColumnName("TestCatagoryID");

                entity.Property(e => e.TestDuration)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TestType)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TestReport>(entity =>
            {
                entity.HasKey(e => e.TestId)
                    .HasName("PK__TestRepo__8CC331000B38C0B7");

                entity.ToTable("TestReport");

                entity.Property(e => e.TestId).HasColumnName("TestID");

                entity.Property(e => e.TestCatagoryId).HasColumnName("TestCatagoryID");

                entity.Property(e => e.TestDate)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.UserName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.TestCatagory)
                    .WithMany(p => p.TestReports)
                    .HasForeignKey(d => d.TestCatagoryId)
                    .HasConstraintName("FK__TestRepor__TestC__5070F446");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.TestReports)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__TestRepor__UserI__4F7CD00D");
            });

            modelBuilder.Entity<UserAnswer>(entity =>
            {
                entity.HasKey(e => e.AnswerId)
                    .HasName("PK__UserAnsw__D4825024A84EE192");

                entity.Property(e => e.AnswerId).HasColumnName("AnswerID");

                entity.Property(e => e.QuestionId).HasColumnName("QuestionID");

                entity.Property(e => e.TestCatagoryId).HasColumnName("TestCatagoryID");

                entity.Property(e => e.TestDate)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UserAnswer1)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("UserAnswer");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Question)
                    .WithMany(p => p.UserAnswers)
                    .HasForeignKey(d => d.QuestionId)
                    .HasConstraintName("FK__UserAnswe__Quest__534D60F1");

                entity.HasOne(d => d.TestCatagory)
                    .WithMany(p => p.UserAnswers)
                    .HasForeignKey(d => d.TestCatagoryId)
                    .HasConstraintName("FK__UserAnswe__TestC__5535A963");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserAnswers)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__UserAnswe__UserI__5441852A");
            });

            modelBuilder.Entity<UserInfo>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK__UserInfo__1788CCAC24A67FBE");

                entity.ToTable("UserInfo");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.EmailId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("EmailID");

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.Property(e => e.UserName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UserPassword)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.UserInfos)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK__UserInfo__RoleID__2C3393D0");
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.HasKey(e => e.RoleId)
                    .HasName("PK__UserRole__8AFACE3A699E21D0");

                entity.ToTable("UserRole");

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.Property(e => e.Discription)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
