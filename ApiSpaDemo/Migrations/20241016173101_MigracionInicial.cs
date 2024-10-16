using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiSpaDemo.Migrations
{
    /// <inheritdoc />
    public partial class MigracionInicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TurnoUsuario");

            migrationBuilder.DropColumn(
                name: "CantActlRsrv",
                table: "Turno");

            migrationBuilder.DropColumn(
                name: "CantMaxRsrv",
                table: "Turno");

            migrationBuilder.DropColumn(
                name: "FechaFinal",
                table: "Turno");

            migrationBuilder.DropColumn(
                name: "Frecuencia",
                table: "Turno");

            migrationBuilder.DropColumn(
                name: "Horario",
                table: "Turno");

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaInicio",
                table: "Turno",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AddColumn<string>(
                name: "Descripcion",
                table: "Turno",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ReservaId",
                table: "Turno",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DuracionMinut",
                table: "Servicio",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "Precio",
                table: "Servicio",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<short>(
                name: "TiempoLimiteHoras",
                table: "Servicio",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AddColumn<int>(
                name: "IsDarkMode",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ChatPrivado",
                columns: table => new
                {
                    ChatId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServicioId = table.Column<int>(type: "int", nullable: false),
                    UsuarioId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatPrivado", x => x.ChatId);
                    table.ForeignKey(
                        name: "FK_ChatPrivado_AspNetUsers_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChatPrivado_Servicio_ServicioId",
                        column: x => x.ServicioId,
                        principalTable: "Servicio",
                        principalColumn: "ServicioId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reserva",
                columns: table => new
                {
                    ReservaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClienteId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NombreIdentificador = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reserva", x => x.ReservaId);
                    table.ForeignKey(
                        name: "FK_Reserva_AspNetUsers_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MensajePrivado",
                columns: table => new
                {
                    MensajePrivadoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChatId = table.Column<int>(type: "int", nullable: true),
                    UsuarioId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Texto = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FechaEnvio = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MensajePrivado", x => x.MensajePrivadoId);
                    table.ForeignKey(
                        name: "FK_MensajePrivado_AspNetUsers_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MensajePrivado_ChatPrivado_ChatId",
                        column: x => x.ChatId,
                        principalTable: "ChatPrivado",
                        principalColumn: "ChatId");
                });

            migrationBuilder.CreateTable(
                name: "Pago",
                columns: table => new
                {
                    PagoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ReservaId = table.Column<int>(type: "int", nullable: false),
                    FormatoPago = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MontoTotal = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    Pagado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pago", x => x.PagoId);
                    table.ForeignKey(
                        name: "FK_Pago_AspNetUsers_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Pago_Reserva_ReservaId",
                        column: x => x.ReservaId,
                        principalTable: "Reserva",
                        principalColumn: "ReservaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Turno_ReservaId",
                table: "Turno",
                column: "ReservaId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatPrivado_ServicioId",
                table: "ChatPrivado",
                column: "ServicioId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatPrivado_UsuarioId",
                table: "ChatPrivado",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_MensajePrivado_ChatId",
                table: "MensajePrivado",
                column: "ChatId");

            migrationBuilder.CreateIndex(
                name: "IX_MensajePrivado_UsuarioId",
                table: "MensajePrivado",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Pago_ReservaId",
                table: "Pago",
                column: "ReservaId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pago_UsuarioId",
                table: "Pago",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Reserva_ClienteId",
                table: "Reserva",
                column: "ClienteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Turno_Reserva_ReservaId",
                table: "Turno",
                column: "ReservaId",
                principalTable: "Reserva",
                principalColumn: "ReservaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Turno_Reserva_ReservaId",
                table: "Turno");

            migrationBuilder.DropTable(
                name: "MensajePrivado");

            migrationBuilder.DropTable(
                name: "Pago");

            migrationBuilder.DropTable(
                name: "ChatPrivado");

            migrationBuilder.DropTable(
                name: "Reserva");

            migrationBuilder.DropIndex(
                name: "IX_Turno_ReservaId",
                table: "Turno");

            migrationBuilder.DropColumn(
                name: "Descripcion",
                table: "Turno");

            migrationBuilder.DropColumn(
                name: "ReservaId",
                table: "Turno");

            migrationBuilder.DropColumn(
                name: "DuracionMinut",
                table: "Servicio");

            migrationBuilder.DropColumn(
                name: "Precio",
                table: "Servicio");

            migrationBuilder.DropColumn(
                name: "TiempoLimiteHoras",
                table: "Servicio");

            migrationBuilder.DropColumn(
                name: "IsDarkMode",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "FechaInicio",
                table: "Turno",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<int>(
                name: "CantActlRsrv",
                table: "Turno",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CantMaxRsrv",
                table: "Turno",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateOnly>(
                name: "FechaFinal",
                table: "Turno",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<string>(
                name: "Frecuencia",
                table: "Turno",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<TimeOnly>(
                name: "Horario",
                table: "Turno",
                type: "time",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0));

            migrationBuilder.CreateTable(
                name: "TurnoUsuario",
                columns: table => new
                {
                    TurnosTurnoId = table.Column<int>(type: "int", nullable: false),
                    UsuariosId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TurnoUsuario", x => new { x.TurnosTurnoId, x.UsuariosId });
                    table.ForeignKey(
                        name: "FK_TurnoUsuario_AspNetUsers_UsuariosId",
                        column: x => x.UsuariosId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TurnoUsuario_Turno_TurnosTurnoId",
                        column: x => x.TurnosTurnoId,
                        principalTable: "Turno",
                        principalColumn: "TurnoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TurnoUsuario_UsuariosId",
                table: "TurnoUsuario",
                column: "UsuariosId");
        }
    }
}
