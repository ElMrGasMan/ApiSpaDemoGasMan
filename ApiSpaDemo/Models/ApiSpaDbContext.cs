﻿using System;
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
    public DbSet<HorarioServicio> HorarioServicio { get; set; } = default!;
    public DbSet<Notificacion> Notificacion { get; set; } = default!;

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


        // CASCADAS DE USUARIO===========================================

        //Chats
        modelBuilder.Entity<Usuario>()
            .HasMany(u => u.ChatsPrivados)
            .WithOne(c => c.UsuarioClass) 
            .OnDelete(DeleteBehavior.Cascade); 
        //Mensajes
        modelBuilder.Entity<Usuario>()
            .HasMany(u => u.MensajesPrivados)
            .WithOne(m => m.Usuario) 
            .OnDelete(DeleteBehavior.Cascade);
        //Notificaciones
        modelBuilder.Entity<Usuario>()
            .HasMany(u => u.Notificaciones)
            .WithOne(n => n.UsuarioClass) 
            .OnDelete(DeleteBehavior.Cascade);
        //Respuestas
        modelBuilder.Entity<Usuario>()
            .HasMany(u => u.Respuestas)
            .WithOne(r => r.UsuarioClass) 
            .OnDelete(DeleteBehavior.Cascade);

        // CASCADAS DE USUARIO===========================================

        // Conversor para TimeOnly
        modelBuilder.Entity<HorarioServicio>()
            .Property(h => h.HoraInicio)
            .HasConversion(
                v => v.ToTimeSpan(), // De TimeOnly a TimeSpan
                v => TimeOnly.FromTimeSpan(v) // De TimeSpan a TimeOnly
            );

        modelBuilder.Entity<HorarioServicio>()
            .Property(h => h.HoraFinal)
            .HasConversion(
                v => v.ToTimeSpan(), // De TimeOnly a TimeSpan
                v => TimeOnly.FromTimeSpan(v) // De TimeSpan a TimeOnly
            );
        //===============================================

        modelBuilder.Entity<ChatPrivado>()
            .HasMany(c => c.Mensajes)
            .WithOne(m => m.ChatPrivado)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Reserva>()
        .HasOne(r => r.Cliente)        
        .WithMany()
        .HasForeignKey(r => r.ClienteId)
        .OnDelete(DeleteBehavior.Restrict);     // Evitar eliminaciones en cascada

       modelBuilder.Entity<MensajePrivado>()
       .HasOne(mp => mp.ChatPrivado)
       .WithMany(cp => cp.Mensajes)
       .HasForeignKey(mp => mp.ChatId)
       .OnDelete(DeleteBehavior.NoAction); // Al eliminar el registro en `ChatPrivado`, `ChatId` en `MensajePrivado` será NULL

        modelBuilder.Entity<Servicio>()
            .Property(s => s.Precio)
            .HasPrecision(10, 2);

        modelBuilder.Entity<Pago>()
            .Property(p => p.MontoTotal)
            .HasPrecision(10, 2);

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
