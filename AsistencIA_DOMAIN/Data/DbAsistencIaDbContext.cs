using System;
using System.Collections.Generic;
using AsistencIA_DOMAIN.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace AsistencIA_DOMAIN.Data;

public partial class DbAsistencIaDbContext : DbContext
{
    public DbAsistencIaDbContext()
    {
    }

    public DbAsistencIaDbContext(DbContextOptions<DbAsistencIaDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Asistencias> Asistencias { get; set; }

    public virtual DbSet<Cursos> Cursos { get; set; }

    public virtual DbSet<Secciones> Secciones { get; set; }

    public virtual DbSet<Sesiones> Sesiones { get; set; }

    public virtual DbSet<Usuarios> Usuarios { get; set; }

    public virtual DbSet<Matriculas> Matriculas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-68O5AFS\\SQLEXPRESS;Database=DB_AsistencIA;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Asistencias>(entity =>
        {
            entity.HasKey(e => e.IdAsistencia).HasName("PK__asistenc__D0454A9A8D0D366A");

            entity.ToTable("asistencias");

            entity.Property(e => e.IdAsistencia).HasColumnName("id_asistencia");
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("estado");
            entity.Property(e => e.IdSesion).HasColumnName("id_sesion");
            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
            entity.Property(e => e.Timestamp)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("timestamp");

            entity.HasOne(d => d.IdSesionNavigation).WithMany(p => p.Asistencias)
                .HasForeignKey(d => d.IdSesion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__asistenci__id_se__4CA06362");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Asistencias)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__asistenci__id_us__4D94879B");
        });

        modelBuilder.Entity<Cursos>(entity =>
        {
            entity.HasKey(e => e.IdCurso).HasName("PK__cursos__5D3F75025593C100");

            entity.ToTable("cursos");

            entity.HasIndex(e => e.Codigo, "UQ__cursos__40F9A20663C6E99B").IsUnique();

            entity.Property(e => e.IdCurso).HasColumnName("id_curso");
            entity.Property(e => e.Codigo)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("codigo");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Secciones>(entity =>
        {
            entity.HasKey(e => e.IdSeccion).HasName("PK__seccione__7C91FD8130358239");

            entity.ToTable("secciones");

            entity.Property(e => e.IdSeccion).HasColumnName("id_seccion");
            entity.Property(e => e.IdCurso).HasColumnName("id_curso");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");

            entity.HasOne(d => d.IdCursoNavigation).WithMany(p => p.Secciones)
                .HasForeignKey(d => d.IdCurso)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__secciones__id_cu__3E52440B");
        });

        modelBuilder.Entity<Sesiones>(entity =>
        {
            entity.HasKey(e => e.IdSesion).HasName("PK__sesiones__8D3F9DFE5F332811");

            entity.ToTable("sesiones");

            entity.Property(e => e.IdSesion).HasColumnName("id_sesion");
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValue("pendiente")
                .HasColumnName("estado");
            entity.Property(e => e.Fecha).HasColumnName("fecha");
            entity.Property(e => e.HoraInicio).HasColumnName("hora_inicio");
            entity.Property(e => e.IdSeccion).HasColumnName("id_seccion");
            entity.Property(e => e.ImagenUrl)
                .HasColumnType("text")
                .HasColumnName("imagen_url");
            entity.Property(e => e.TipoIngreso)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("tipo_ingreso");

            entity.HasOne(d => d.IdSeccionNavigation).WithMany(p => p.Sesiones)
                .HasForeignKey(d => d.IdSeccion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__sesiones__id_sec__47DBAE45");
        });

        modelBuilder.Entity<Usuarios>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__usuarios__4E3E04ADD745E63C");

            entity.ToTable("usuarios");

            entity.HasIndex(e => e.Email, "UQ__usuarios__AB6E61646F8DCB37").IsUnique();

            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
            entity.Property(e => e.Contrasena)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("contrasena");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.FotoReferencia)
                .HasColumnType("text")
                .HasColumnName("foto_referencia");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Rol)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("rol");

            entity.HasMany(d => d.IdSeccion).WithMany(p => p.IdUsuario)
                .UsingEntity<Dictionary<string, object>>(
                    "Matriculas",
                    r => r.HasOne<Secciones>().WithMany()
                        .HasForeignKey("IdSeccion")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__matricula__id_se__4222D4EF"),
                    l => l.HasOne<Usuarios>().WithMany()
                        .HasForeignKey("IdUsuario")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__matricula__id_us__412EB0B6"),
                    j =>
                    {
                        j.HasKey("IdUsuario", "IdSeccion").HasName("PK__matricul__49F71B75913B9D38");
                        j.ToTable("matriculas");
                        j.IndexerProperty<int>("IdUsuario").HasColumnName("id_usuario");
                        j.IndexerProperty<int>("IdSeccion").HasColumnName("id_seccion");
                    });
        });

        modelBuilder.Entity<Matriculas>(entity =>
        {
            entity.HasKey(e => new { e.IdUsuario, e.IdSeccion }); // clave compuesta

            entity.Property(e => e.IdUsuario)
                  .HasColumnName("id_usuario")
                  .HasMaxLength(100) // Usa el mismo tamaño que en tu DB
                  .IsRequired();

            entity.Property(e => e.IdSeccion)
                  .HasColumnName("id_seccion");

            // Relaciones
            entity.HasOne(d => d.Usuario)
                  .WithMany(p => p.Matriculas)
                  .HasForeignKey(d => d.IdUsuario)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(d => d.Seccion)
                  .WithMany(p => p.Matriculas)
                  .HasForeignKey(d => d.IdSeccion)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
