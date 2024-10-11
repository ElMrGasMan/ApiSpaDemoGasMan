using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

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
    public DbSet<ChatPrivado> ChatPrivado { get; set; } = default!; 
    public DbSet<MensajePrivado> MensajePrivado { get; set; } = default!;
    public DbSet<Pago> Pago { get; set; } = default!;
    public DbSet<Reserva> Reserva { get; set; } = default!;
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

        modelBuilder.Entity<Reserva>()
        .HasOne(r => r.Cliente)        
        .WithMany()
        .HasForeignKey(r => r.ClienteId)
        .OnDelete(DeleteBehavior.Restrict);     // Evitar eliminaciones en cascada

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
