using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace School.Data.Models;

public partial class OigaDbContext : DbContext
{
    public OigaDbContext()
    {
    }

    public OigaDbContext(DbContextOptions<OigaDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<CourseStudent> CourseStudents { get; set; }

    public virtual DbSet<Evaluation> Evaluations { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer("Name=DefaultConnection");
        }
    }   

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Course>(entity =>
        {
            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Active).HasColumnName("active");
            entity.Property(e => e.CreationDate)
                .HasColumnType("date")
                .HasColumnName("creation_date");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        modelBuilder.Entity<CourseStudent>(entity =>
        {
            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CourseId).HasColumnName("course_id");
            entity.Property(e => e.Grade).HasColumnName("grade");
            entity.Property(e => e.StudentId).HasColumnName("student_id");

            entity.HasOne(d => d.Course).WithMany(p => p.CourseStudents)
                .HasForeignKey(d => d.CourseId)
                .HasConstraintName("FK_CourseStudents_Courses");

            entity.HasOne(d => d.Student).WithMany(p => p.CourseStudents)
                .HasForeignKey(d => d.StudentId)
                .HasConstraintName("FK_CourseStudents_Students");
        });

        modelBuilder.Entity<Evaluation>(entity =>
        {
            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CourseStudentId).HasColumnName("course_student_id");
            entity.Property(e => e.CreationDate)
                .HasColumnType("date")
                .HasColumnName("creation_date");
            entity.Property(e => e.Description)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.Stars).HasColumnName("stars");

            entity.HasOne(d => d.CourseStudent).WithMany(p => p.Evaluations)
                .HasForeignKey(d => d.CourseStudentId)
                .HasConstraintName("FK_Evaluations_CourseStudents");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Active).HasColumnName("active");
            entity.Property(e => e.CreationDate)
                .HasColumnType("date")
                .HasColumnName("creation_date");
            entity.Property(e => e.LastName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("last_name");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
