using ApiSpaDemo.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.EntityFrameworkCore;

namespace ApiSpaDemo.Services
{
    [EnableCors("PermitirTodo")]
    public class TurnoCleanupService : IHostedService, IDisposable
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private Timer _timer;

        public TurnoCleanupService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(VerificarTurnosNoPagados, null, TimeSpan.Zero, TimeSpan.FromHours(1));
            return Task.CompletedTask;
        }

        //private async void VerificarTurnosNoPagados(object state)
        //{
        //    using (var scope = _scopeFactory.CreateScope())
        //    {
        //        var _context = scope.ServiceProvider.GetRequiredService<ApiSpaDbContext>();

        //        // Obtener todas las reservas que no tienen pago confirmado
        //        var reservasSinPago = await _context.Reserva
        //            .Include(r => r.Pago)
        //            .Include(r => r.Turnos)
        //            .ThenInclude(t => t.ServicioClass)
        //            .Where(r => !r.Pago.Pagado)
        //            .ToListAsync();

        //        foreach (var reserva in reservasSinPago)
        //        {
        //            foreach (var turno in reserva.Turnos)
        //            {
        //                var fechaLimite = turno.FechaInicio.AddHours(-turno.ServicioClass.TiempoLimiteHoras);
        //                if (DateTime.Now >= fechaLimite)
        //                {
        //                    // Si la fecha límite ha pasado, eliminar el turno de la reserva
        //                    reserva.Turnos.Remove(turno);
        //                    turno.ReservaId = null;

        //                    // Arreglar el precio del Pago; restarle lo que valia el turno.
        //                    reserva.Pago.MontoTotal -= turno.ServicioClass.Precio;
        //                }
        //            }
        //        }

        //        await _context.SaveChangesAsync();
        //    }
        //}
        private async void VerificarTurnosNoPagados(object state)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var _context = scope.ServiceProvider.GetRequiredService<ApiSpaDbContext>();

                // Obtener todas las reservas que no tienen pago confirmado
                var reservasSinPago = await _context.Reserva
                    .Include(r => r.Pago)
                    .Include(r => r.Turnos)
                    .ThenInclude(t => t.ServicioClass)
                    .Where(r => !r.Pago.Pagado)
                    .ToListAsync();

                foreach (var reserva in reservasSinPago)
                {
                    // Lista temporal para almacenar los turnos que deben ser eliminados
                    var turnosAEliminar = new List<Turno>();

                    foreach (var turno in reserva.Turnos)
                    {
                        var fechaLimite = turno.FechaInicio.AddHours(-turno.ServicioClass.TiempoLimiteHoras);
                        if (DateTime.Now >= fechaLimite)
                        {
                            // Si la fecha límite ha pasado, agregar el turno a la lista de eliminaciones
                            turnosAEliminar.Add(turno);

                            // Arreglar el precio del Pago; restarle lo que valía el turno.
                            reserva.Pago.MontoTotal -= turno.ServicioClass.Precio;
                        }
                    }

                    // Eliminar los turnos fuera del bucle foreach
                    foreach (var turno in turnosAEliminar)
                    {
                        reserva.Turnos.Remove(turno);
                        turno.ReservaId = null;
                    }
                }

                await _context.SaveChangesAsync();
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
