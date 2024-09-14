using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ApiSpaDemo.Models;

public partial class ApiSpaDbContext : IdentityDbContext<Usuario, IdentityRole, string>
{
    public ApiSpaDbContext()
    {
    }

    public ApiSpaDbContext(DbContextOptions<ApiSpaDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Noticium> Noticia { get; set; }
    public DbSet<Resenia> Resenia { get; set; } = default!;
    public DbSet<Pregunta> Pregunta { get; set; } = default!;
    public DbSet<Respuesta> Respuesta { get; set; } = default!;
    public DbSet<Servicio> Servicio { get; set; } = default!;
    public DbSet<Turno> Turno { get; set; } = default!;


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Noticium>(entity =>
        {
            entity.HasKey(e => e.NoticiaId).HasName("PK__Noticia__F33000CFEC4BAB94");

            entity.Property(e => e.RutaPdf).HasColumnName("RutaPDF");
            entity.Property(e => e.Titulo).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
