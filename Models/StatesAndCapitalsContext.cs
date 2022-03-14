using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace QuizProject.Models
{
    public partial class StatesAndCapitalsContext : DbContext
    {
        public StatesAndCapitalsContext()
        {
        }

        public StatesAndCapitalsContext(DbContextOptions<StatesAndCapitalsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<State> States { get; set; }
        public virtual DbSet<TestResult> TestResults { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=localhost;Database=StatesAndCapitals;User ID=sa;Password=SApassword;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<State>(entity =>
            {
                entity.ToTable("State");

                entity.Property(e => e.Capital).HasMaxLength(255);

                entity.Property(e => e.State1)
                    .HasMaxLength(255)
                    .HasColumnName("State");
            });

            modelBuilder.Entity<TestResult>(entity =>
            {
                entity.ToTable("TestResult");

                entity.Property(e => e.TestDateTime).HasColumnType("datetime");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.TestResults)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_TestResult_User");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.Password).HasMaxLength(255);

                entity.Property(e => e.UserName).HasMaxLength(255);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
