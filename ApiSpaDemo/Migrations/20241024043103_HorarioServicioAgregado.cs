using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiSpaDemo.Migrations
{
    /// <inheritdoc />
    public partial class HorarioServicioAgregado : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "FormatoPago",
                table: "Pago",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaPagado",
                table: "Pago",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "HorarioServicio",
                columns: table => new
                {
                    HorarioServicioId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServicioId = table.Column<int>(type: "int", nullable: true),
                    HoraInicio = table.Column<TimeOnly>(type: "time", nullable: false),
                    HoraFinal = table.Column<TimeOnly>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HorarioServicio", x => x.HorarioServicioId);
                    table.ForeignKey(
                        name: "FK_HorarioServicio_Servicio_ServicioId",
                        column: x => x.ServicioId,
                        principalTable: "Servicio",
                        principalColumn: "ServicioId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_HorarioServicio_ServicioId",
                table: "HorarioServicio",
                column: "ServicioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HorarioServicio");

            migrationBuilder.DropColumn(
                name: "FechaPagado",
                table: "Pago");

            migrationBuilder.AlterColumn<string>(
                name: "FormatoPago",
                table: "Pago",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
