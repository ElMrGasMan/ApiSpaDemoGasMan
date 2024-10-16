﻿using ApiSpaDemo.Models.DTO;
using ApiSpaDemo.Models;
using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace ApiSpaDemo.Controllers
{
    [EnableCors("ReglasCors")]
    [Route("api/[controller]")]
    [ApiController]
    public class ReservaController : ControllerBase
    {
        private readonly ApiSpaDbContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<Usuario> _userManager;

        public ReservaController(ApiSpaDbContext context, IMapper mapper, UserManager<Usuario> userManager)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
        }

        // GET: api/Reserva
        // Obtiene todas las reservas de todos los usuarios
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ReservaDTO>>> GetReserva()
        {
            var reservas = await _context.Reserva.ToListAsync();
            var reservasDTO = _mapper.Map<List<ReservaDTO>>(reservas);
            return Ok(reservasDTO);
        }

        // GET: api/Reserva
        // Obtiene todas las reservas de todos los usuarios con los turnos
        [HttpGet("reservAllWTurnos")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ReservaDTO>>> GetReservaTurnos()
        {
            var reservas = await _context.Reserva
                .Include(r => r.Turnos) // Incluir Turnos
                .Include(r => r.Pago) // Y el pago
                .ToListAsync();
            var reservasDTO = _mapper.Map<List<ReservaDTO>>(reservas);
            return Ok(reservasDTO);
        }

        // GET: api/Reserva
        // Verifica todas las reservas, que los turnos no hayan pasado el limite y no esten pagados.
        [HttpGet("verificacionPagoDeReservas")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ReservaDTO>>> GetReservaTurnosVerfic()
        {
            // Obtener todas las reservas que no tienen pago confirmado
            var reservasSinPago = await _context.Reserva
                .Include(r => r.Pago)
                .Include(r => r.Turnos)
                .ThenInclude(t => t.ServicioClass)
                .Where(r => !r.Pago.Pagado)
                .ToListAsync();

            foreach (var reserva in reservasSinPago)
            {
                foreach (var turno in reserva.Turnos)
                {
                    var fechaLimite = turno.FechaInicio.AddHours(-turno.ServicioClass.TiempoLimiteHoras);
                    if (DateTime.Now >= fechaLimite)
                    {
                        // Si la fecha límite ha pasado, eliminar el turno de la reserva
                        reserva.Turnos.Remove(turno);
                        turno.ReservaId = null;

                        // Arreglar el precio del Pago; restarle lo que valia el turno.
                        reserva.Pago.MontoTotal -= turno.ServicioClass.Precio;
                    }
                }
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error en la verificación: {ex.Message}");
            }

            return Ok("Pagos de reservas verificados exitosamente.");
        }

        // GET: api/Reserva
        // Obtiene todas las reservas de todos los usuarios de forma Limitada
        [HttpGet("reservAllLimited")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ReservaDTO>>> GetReservaLimited(int saltear, int tomar)
        {
            var reservas = await _context.Reserva
                .Skip(saltear)
                .Take(tomar)
                .ToListAsync();
            var reservasDTO = _mapper.Map<List<ReservaDTO>>(reservas);
            return Ok(reservasDTO);
        }


        // GET: api/Reserva/5
        // Obtiene todas las reservas de un cierto cliente
        [HttpGet("reservUserAll")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ReservaDTO>>> GetReservaUsuario(string clienteId)
        {
            var reservas = await _context.Reserva
                                   .Where(pag => pag.ClienteId == clienteId)
                                   .ToListAsync();
            var reservasDTO = _mapper.Map<List<ReservaDTO>>(reservas);
            return Ok(reservasDTO);
        }


        // GET: api/Reserva
        // Obtiene todas las reservas del cliente autenticado
        [HttpGet("reservAuthUserAll")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ReservaSimpleDTO>>> GetReservaUsuarioAuth()
        {
            var usuario = await _userManager.GetUserAsync(User);
            if (usuario == null)
            {
                return Unauthorized();
            }

            var reservas = await _context.Reserva
                .Where(pag => pag.ClienteId == usuario.Id)
                .ToListAsync();
            var reservasSimplesDTO = _mapper.Map<List<ReservaSimpleDTO>>(reservas);

            return Ok(reservasSimplesDTO);
        }

        // GET: api/Reserva/5
        // Obtiene una reserva especifica 
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ReservaDTO>> GetReserva(int id)
        {
            var reserva = await _context.Reserva
                .Include(r => r.Turnos) // Incluir Turnos
                .Include(r => r.Pago) // Y el pago
                .FirstOrDefaultAsync(r => r.ReservaId == id);

            if (reserva == null)
            {
                return NotFound();
            }

            var reservaDTO = _mapper.Map<ReservaDTO>(reserva);
            return Ok(reservaDTO);
        }


        // GET: api/Reserva/5
        // Obtiene una reserva especifica del usuario autenticado
        [HttpGet("reservaAuthUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ReservaDTO>> GetReservaServUser(int id)
        {
            var usuario = await _userManager.GetUserAsync(User);
            if (usuario == null)
            {
                return Unauthorized();
            }

            var reserva = await _context.Reserva
                .Include(r => r.Turnos) // Incluir Turnos
                .Include(r => r.Pago) // Y el pago
                .Where(r => r.ClienteId == usuario.Id)
                .FirstOrDefaultAsync(r => r.ReservaId == id);

            if (reserva == null)
            {
                return NotFound();
            }

            var reservaDTO = _mapper.Map<ReservaDTO>(reserva);
            return Ok(reservaDTO);
        }


        // POST: api/Reserva
        // Crea una nueva Reserva y su correspondiente Pago, por lo tanto, tambien se pide el monto y el formato de pago
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ReservaSimpleDTO>> PostReserva(ReservaSimpleDTO reservaSimpleDTO, decimal monto, string formatoPago)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var usuario = await _userManager.GetUserAsync(User);
            if (usuario == null)
            {
                return Unauthorized();
            }

            var reserva = _mapper.Map<Reserva>(reservaSimpleDTO);
            reserva.ClienteId = usuario.Id;

            var pago = new Pago
            {
                ReservaClass = reserva,
                UsuarioClass = await _userManager.GetUserAsync(User),
                FormatoPago = formatoPago,
                MontoTotal = monto
            };

            await _context.Reserva.AddAsync(reserva);
            await _context.Pago.AddAsync(pago);
            await _context.SaveChangesAsync();

            var reservaToReturn = _mapper.Map<ReservaSimpleDTO>(reserva);
            return CreatedAtAction(nameof(GetReserva), new { id = reservaToReturn.ReservaId }, reservaToReturn);
        }


        // PUT: api/Reserva
        // Agrega un nuevo turno a la Reserva
        [HttpPut("agregarTurnoAReserva/{idReserva}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AgregarTurnoAReserva(int idReserva, int idTurno)
        {
            var reserva = await _context.Reserva
                .Include(r => r.Turnos) // Incluir los turnos 
                .FirstOrDefaultAsync(r => r.ReservaId == idReserva);

            if (reserva == null)
            {
                return NotFound($"No se encontró una reserva con el ID {idReserva}.");
            }

            var turno = await _context.Turno.FindAsync(idTurno);
            if (turno == null)
            {
                return NotFound($"No se encontró un turno con el ID {idTurno}");
            }

            // Verificar si el turno ya está asignado a una reserva
            if (turno.ReservaId != null)
            {
                return BadRequest($"El turno con ID {idTurno} ya está asignado a una reserva.");
            }

            turno.ReservaId = idReserva;
            reserva.Turnos.Add(turno);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error al actualizar la reserva: {ex.Message}");
            }

            return Ok($"El turno con ID {idTurno} ha sido agregado a la reserva con ID {idReserva}");
        }


        // PUT: api/Reserva
        // Elimina un turno de la Reserva
        [HttpPut("eliminarTurnoAReserva/{idReserva}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> EliminarTurnoAReserva(int idReserva, int idTurno)
        {
            Reserva? reserva = await _context.Reserva
                .Include(r => r.Turnos) // Incluir los turnos 
                .FirstOrDefaultAsync(r => r.ReservaId == idReserva);

            if (reserva == null)
            {
                return NotFound($"No se encontró una reserva con el ID {idReserva}.");
            }

            Turno? turno = await _context.Turno.FindAsync(idTurno);
            if (turno == null)
            {
                return NotFound($"No se encontró un turno con el ID {idTurno}");
            }

            reserva.Turnos.Remove(turno);
            turno.ReservaId = null;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error al actualizar la reserva: {ex.Message}");
            }

            return Ok($"El turno con ID {idTurno} ha sido eliminado a la reserva con ID {idReserva}");
        }


        // DELETE: api/Reserva/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteReserva(int id)
        {
            var reserva = await _context.Reserva.FindAsync(id);
            if (reserva == null)
            {
                return NotFound();
            }

            _context.Reserva.Remove(reserva);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
