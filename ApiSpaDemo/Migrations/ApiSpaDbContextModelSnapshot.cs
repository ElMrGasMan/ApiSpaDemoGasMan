﻿// <auto-generated />
using System;
using ApiSpaDemo.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ApiSpaDemo.Migrations
{
    [DbContext(typeof(ApiSpaDbContext))]
    partial class ApiSpaDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ApiSpaDemo.Models.ChatPrivado", b =>
                {
                    b.Property<int>("ChatId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ChatId"));

                    b.Property<int>("ServicioId")
                        .HasColumnType("int");

                    b.Property<string>("UsuarioId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("ChatId");

                    b.HasIndex("ServicioId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("ChatPrivado");
                });

            modelBuilder.Entity("ApiSpaDemo.Models.HorarioServicio", b =>
                {
                    b.Property<int>("HorarioServicioId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("HorarioServicioId"));

                    b.Property<TimeSpan>("HoraFinal")
                        .HasColumnType("time");

                    b.Property<TimeSpan>("HoraInicio")
                        .HasColumnType("time");

                    b.Property<int?>("ServicioId")
                        .HasColumnType("int");

                    b.HasKey("HorarioServicioId");

                    b.HasIndex("ServicioId");

                    b.ToTable("HorarioServicio");
                });

            modelBuilder.Entity("ApiSpaDemo.Models.MensajePrivado", b =>
                {
                    b.Property<int>("MensajePrivadoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MensajePrivadoId"));

                    b.Property<int?>("ChatId")
                        .HasColumnType("int");

                    b.Property<DateTime>("FechaEnvio")
                        .HasColumnType("datetime2");

                    b.Property<string>("Texto")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("UsuarioId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("MensajePrivadoId");

                    b.HasIndex("ChatId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("MensajePrivado");
                });

            modelBuilder.Entity("ApiSpaDemo.Models.Noticium", b =>
                {
                    b.Property<int>("NoticiaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("NoticiaId"));

                    b.Property<DateOnly>("FechaPublicacion")
                        .HasColumnType("date");

                    b.Property<string>("RutaImagen")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RutaPdf")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("RutaPDF");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("NoticiaId")
                        .HasName("PK__Noticia__F33000CFEC4BAB94");

                    b.ToTable("Noticia");
                });

            modelBuilder.Entity("ApiSpaDemo.Models.Notificacion", b =>
                {
                    b.Property<int>("NotificacionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("NotificacionId"));

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<bool>("Leido")
                        .HasColumnType("bit");

                    b.Property<string>("UsuarioId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("NotificacionId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Notificacion");
                });

            modelBuilder.Entity("ApiSpaDemo.Models.Pago", b =>
                {
                    b.Property<int>("PagoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PagoId"));

                    b.Property<DateTime?>("FechaPagado")
                        .HasColumnType("datetime2");

                    b.Property<string>("FormatoPago")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("MontoTotal")
                        .HasPrecision(10, 2)
                        .HasColumnType("decimal(10,2)");

                    b.Property<bool>("Pagado")
                        .HasColumnType("bit");

                    b.Property<int?>("ReservaId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<string>("UsuarioId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("PagoId");

                    b.HasIndex("ReservaId")
                        .IsUnique();

                    b.HasIndex("UsuarioId");

                    b.ToTable("Pago");
                });

            modelBuilder.Entity("ApiSpaDemo.Models.Pregunta", b =>
                {
                    b.Property<int>("PreguntaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PreguntaId"));

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateOnly>("FechaPublicacion")
                        .HasColumnType("date");

                    b.Property<string>("UsuarioId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("PreguntaId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Pregunta");
                });

            modelBuilder.Entity("ApiSpaDemo.Models.Resenia", b =>
                {
                    b.Property<int>("ReseniaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ReseniaId"));

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<DateTime>("FechaPublicacion")
                        .HasColumnType("datetime2");

                    b.Property<short>("Puntuacion")
                        .HasColumnType("smallint");

                    b.Property<string>("UsuarioId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("ReseniaId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Resenia");
                });

            modelBuilder.Entity("ApiSpaDemo.Models.Reserva", b =>
                {
                    b.Property<int>("ReservaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ReservaId"));

                    b.Property<string>("ClienteId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("NombreIdentificador")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.HasKey("ReservaId");

                    b.HasIndex("ClienteId");

                    b.ToTable("Reserva");
                });

            modelBuilder.Entity("ApiSpaDemo.Models.Respuesta", b =>
                {
                    b.Property<int>("RespuestaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RespuestaId"));

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<int>("PreguntaId")
                        .HasColumnType("int");

                    b.Property<string>("UsuarioId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("RespuestaId");

                    b.HasIndex("PreguntaId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Respuesta");
                });

            modelBuilder.Entity("ApiSpaDemo.Models.Servicio", b =>
                {
                    b.Property<int>("ServicioId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ServicioId"));

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasMaxLength(600)
                        .HasColumnType("nvarchar(600)");

                    b.Property<int>("DuracionMinut")
                        .HasColumnType("int");

                    b.Property<decimal>("Precio")
                        .HasPrecision(10, 2)
                        .HasColumnType("decimal(10,2)");

                    b.Property<string>("RutaImagen")
                        .HasColumnType("nvarchar(max)");

                    b.Property<short>("TiempoLimiteHoras")
                        .HasColumnType("smallint");

                    b.Property<string>("TipoServicio")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("UsuarioId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("ServicioId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Servicio");
                });

            modelBuilder.Entity("ApiSpaDemo.Models.Turno", b =>
                {
                    b.Property<int>("TurnoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TurnoId"));

                    b.Property<string>("Descripcion")
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<DateTime>("FechaInicio")
                        .HasColumnType("datetime2");

                    b.Property<int?>("ReservaId")
                        .HasColumnType("int");

                    b.Property<int>("ServicioId")
                        .HasColumnType("int");

                    b.HasKey("TurnoId");

                    b.HasIndex("ReservaId");

                    b.HasIndex("ServicioId");

                    b.ToTable("Turno");
                });

            modelBuilder.Entity("ApiSpaDemo.Models.Usuario", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<int>("IsDarkMode")
                        .HasColumnType("int");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("ApiSpaDemo.Models.ChatPrivado", b =>
                {
                    b.HasOne("ApiSpaDemo.Models.Servicio", "Servicio")
                        .WithMany("ChatsPrivados")
                        .HasForeignKey("ServicioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ApiSpaDemo.Models.Usuario", "UsuarioClass")
                        .WithMany("ChatsPrivados")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Servicio");

                    b.Navigation("UsuarioClass");
                });

            modelBuilder.Entity("ApiSpaDemo.Models.HorarioServicio", b =>
                {
                    b.HasOne("ApiSpaDemo.Models.Servicio", "ServicioClass")
                        .WithMany("Horarios")
                        .HasForeignKey("ServicioId");

                    b.Navigation("ServicioClass");
                });

            modelBuilder.Entity("ApiSpaDemo.Models.MensajePrivado", b =>
                {
                    b.HasOne("ApiSpaDemo.Models.ChatPrivado", "ChatPrivado")
                        .WithMany("Mensajes")
                        .HasForeignKey("ChatId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("ApiSpaDemo.Models.Usuario", "Usuario")
                        .WithMany("MensajesPrivados")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ChatPrivado");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("ApiSpaDemo.Models.Notificacion", b =>
                {
                    b.HasOne("ApiSpaDemo.Models.Usuario", "UsuarioClass")
                        .WithMany("Notificaciones")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UsuarioClass");
                });

            modelBuilder.Entity("ApiSpaDemo.Models.Pago", b =>
                {
                    b.HasOne("ApiSpaDemo.Models.Reserva", "ReservaClass")
                        .WithOne("Pago")
                        .HasForeignKey("ApiSpaDemo.Models.Pago", "ReservaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ApiSpaDemo.Models.Usuario", "UsuarioClass")
                        .WithMany()
                        .HasForeignKey("UsuarioId");

                    b.Navigation("ReservaClass");

                    b.Navigation("UsuarioClass");
                });

            modelBuilder.Entity("ApiSpaDemo.Models.Pregunta", b =>
                {
                    b.HasOne("ApiSpaDemo.Models.Usuario", "UsuarioClass")
                        .WithMany("Preguntas")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UsuarioClass");
                });

            modelBuilder.Entity("ApiSpaDemo.Models.Resenia", b =>
                {
                    b.HasOne("ApiSpaDemo.Models.Usuario", "UsuarioClass")
                        .WithMany("Resenias")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UsuarioClass");
                });

            modelBuilder.Entity("ApiSpaDemo.Models.Reserva", b =>
                {
                    b.HasOne("ApiSpaDemo.Models.Usuario", "Cliente")
                        .WithMany()
                        .HasForeignKey("ClienteId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Cliente");
                });

            modelBuilder.Entity("ApiSpaDemo.Models.Respuesta", b =>
                {
                    b.HasOne("ApiSpaDemo.Models.Pregunta", "PreguntaClass")
                        .WithMany()
                        .HasForeignKey("PreguntaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ApiSpaDemo.Models.Usuario", "UsuarioClass")
                        .WithMany("Respuestas")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PreguntaClass");

                    b.Navigation("UsuarioClass");
                });

            modelBuilder.Entity("ApiSpaDemo.Models.Servicio", b =>
                {
                    b.HasOne("ApiSpaDemo.Models.Usuario", "UsuarioClass")
                        .WithMany("Servicios")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UsuarioClass");
                });

            modelBuilder.Entity("ApiSpaDemo.Models.Turno", b =>
                {
                    b.HasOne("ApiSpaDemo.Models.Reserva", "ReservaClass")
                        .WithMany("Turnos")
                        .HasForeignKey("ReservaId");

                    b.HasOne("ApiSpaDemo.Models.Servicio", "ServicioClass")
                        .WithMany("Turnos")
                        .HasForeignKey("ServicioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ReservaClass");

                    b.Navigation("ServicioClass");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("ApiSpaDemo.Models.Usuario", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("ApiSpaDemo.Models.Usuario", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ApiSpaDemo.Models.Usuario", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("ApiSpaDemo.Models.Usuario", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ApiSpaDemo.Models.ChatPrivado", b =>
                {
                    b.Navigation("Mensajes");
                });

            modelBuilder.Entity("ApiSpaDemo.Models.Reserva", b =>
                {
                    b.Navigation("Pago");

                    b.Navigation("Turnos");
                });

            modelBuilder.Entity("ApiSpaDemo.Models.Servicio", b =>
                {
                    b.Navigation("ChatsPrivados");

                    b.Navigation("Horarios");

                    b.Navigation("Turnos");
                });

            modelBuilder.Entity("ApiSpaDemo.Models.Usuario", b =>
                {
                    b.Navigation("ChatsPrivados");

                    b.Navigation("MensajesPrivados");

                    b.Navigation("Notificaciones");

                    b.Navigation("Preguntas");

                    b.Navigation("Resenias");

                    b.Navigation("Respuestas");

                    b.Navigation("Servicios");
                });
#pragma warning restore 612, 618
        }
    }
}
