using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MVCCRUD.Models
{
    public partial class graceContext : DbContext
    {
        public graceContext()
        {
        }

        public graceContext(DbContextOptions<graceContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Alumno> Alumnos { get; set; } = null!;
        public virtual DbSet<Materia> Materias { get; set; } = null!;
        public virtual DbSet<Profesore> Profesores { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
 //               optionsBuilder.UseSqlServer("Server=DESKTOP-LI62I77\\SQLEXPRESS01; DataBase=grace;Integrated Security=true; encrypt=false");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Alumno>(entity =>
            {
                entity.HasKey(e => e.IdAlumno)
                    .HasName("PK__Alumnos__6D77A7F1E22D3CE0");

                entity.Property(e => e.IdAlumno)
                    .ValueGeneratedNever()
                    .HasColumnName("id_alumno");

                entity.Property(e => e.IdMateria).HasColumnName("id_materia");

                entity.Property(e => e.IdProfesor).HasColumnName("id_profesor");

                entity.Property(e => e.NombreAlumno)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("nombre_alumno");

                entity.HasOne(d => d.IdMateriaNavigation)
                    .WithMany(p => p.Alumnos)
                    .HasForeignKey(d => d.IdMateria)
                    .HasConstraintName("FK__Alumnos__id_mate__3B75D760");

                entity.HasOne(d => d.IdProfesorNavigation)
                    .WithMany(p => p.Alumnos)
                    .HasForeignKey(d => d.IdProfesor)
                    .HasConstraintName("FK__Alumnos__id_prof__3C69FB99");
            });

            modelBuilder.Entity<Materia>(entity =>
            {
                entity.HasKey(e => e.IdMateria)
                    .HasName("PK__Materias__7E03FD39740FF815");

                entity.Property(e => e.IdMateria)
                    .ValueGeneratedNever()
                    .HasColumnName("id_materia");

                entity.Property(e => e.NombreMateria)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("nombre_materia");
            });

            modelBuilder.Entity<Profesore>(entity =>
            {
                entity.HasKey(e => e.IdProfesor)
                    .HasName("PK__Profesor__159ED6177626BDE0");

                entity.Property(e => e.IdProfesor)
                    .ValueGeneratedNever()
                    .HasColumnName("id_profesor");

                entity.Property(e => e.NombreProfesor)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("nombre_profesor");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
