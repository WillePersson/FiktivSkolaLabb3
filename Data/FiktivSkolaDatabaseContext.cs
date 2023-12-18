using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using FiktivSkolaLabb._2.Models;

namespace FiktivSkolaLabb._2.Data
{
    public partial class FiktivSkolaDatabaseContext : DbContext
    {
        public FiktivSkolaDatabaseContext()
        {
        }

        public FiktivSkolaDatabaseContext(DbContextOptions<FiktivSkolaDatabaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Admin> Admins { get; set; } = null!;
        public virtual DbSet<Class> Classes { get; set; } = null!;
        public virtual DbSet<Course> Courses { get; set; } = null!;
        public virtual DbSet<CourseGrade> CourseGrades { get; set; } = null!;
        public virtual DbSet<Janitor> Janitors { get; set; } = null!;
        public virtual DbSet<Personal> Personals { get; set; } = null!;
        public virtual DbSet<Principal> Principals { get; set; } = null!;
        public virtual DbSet<Student> Students { get; set; } = null!;
        public virtual DbSet<Teacher> Teachers { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=FiktivSkolaDatabase;Integrated Security=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Admin>(entity =>
            {
                entity.HasOne(d => d.FkPersonal)
                    .WithMany(p => p.Admins)
                    .HasForeignKey(d => d.FkPersonalId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK Personal in Admin");
            });

            modelBuilder.Entity<Class>(entity =>
            {
                entity.HasOne(d => d.FkTeacher)
                    .WithMany(p => p.Classes)
                    .HasForeignKey(d => d.FkTeacherId)
                    .HasConstraintName("FK Teacher In Class");
            });

            modelBuilder.Entity<CourseGrade>(entity =>
            {
                entity.HasOne(d => d.FkCourse)
                    .WithMany()
                    .HasForeignKey(d => d.FkCourseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Course_Grades_Course");

                entity.HasOne(d => d.FkStudent)
                    .WithMany()
                    .HasForeignKey(d => d.FkStudentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Course_Grades_Student");

                entity.HasOne(d => d.FkTeacher)
                    .WithMany()
                    .HasForeignKey(d => d.FkTeacherId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Course_Grades_Teacher");
            });

            modelBuilder.Entity<Janitor>(entity =>
            {
                entity.HasOne(d => d.FkPersonal)
                    .WithMany(p => p.Janitors)
                    .HasForeignKey(d => d.FkPersonalId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK Perosnal in Janitor");
            });

            modelBuilder.Entity<Personal>(entity =>
            {
                entity.Property(e => e.PersonalId).ValueGeneratedNever();
            });

            modelBuilder.Entity<Principal>(entity =>
            {
                entity.HasOne(d => d.FkPersonal)
                    .WithMany(p => p.Principals)
                    .HasForeignKey(d => d.FkPersonalId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK Personal In Principal");
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasOne(d => d.FkClass)
                    .WithMany(p => p.Students)
                    .HasForeignKey(d => d.FkClassId)
                    .HasConstraintName("FK Class in Student");
            });

            modelBuilder.Entity<Teacher>(entity =>
            {
                entity.HasOne(d => d.FkClass)
                    .WithMany(p => p.Teachers)
                    .HasForeignKey(d => d.FkClassId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK Class in Teacher");

                entity.HasOne(d => d.FkPersonal)
                    .WithMany(p => p.Teachers)
                    .HasForeignKey(d => d.FkPersonalId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK Personal In Teacher");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
