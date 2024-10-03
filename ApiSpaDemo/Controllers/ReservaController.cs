using ApiSpaDemo.Models.DTO;
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

        // GET: api/Reserva/5
        // Obtiene todas las reservas de un cierto cliente
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ReservaDTO>>> GetReservaUsuario(string clienteId)
        {
            var reservas = await _context.Reserva
                                   .Where(pag => pag.ClienteId == clienteId)
                                   .ToListAsync();
            var reservasDTO = _mapper.Map<List<ReservaDTO>>(reservas);
            return Ok(reservasDTO);
        }

        // GET: api/Reserva/5
        // Obtiene una reserva especifica 
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ReservaDTO>> GetReserva(int id)
        {
            var reserva = await _context.Reserva.FindAsync(id);

            if (reserva == null)
            {
                return NotFound();
            }

            var reservaDTO = _mapper.Map<ReservaDTO>(reserva);
            return Ok(reservaDTO);
        }


        // GET: api/Reserva/5
        // Obtiene una reserva de un servicio específico del usuario autenticado
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ReservaDTO>> GetReservaServUser(int servicioId)
        {
            var usuario = await _userManager.GetUserAsync(User);
            if (usuario == null)
            {
                return Unauthorized();
            }

            var reserva = await _context.Reserva
                                   .Where(reserv => reserv.ServicioId == servicioId && reserv.ClienteId == usuario.Id)
                                   .FirstOrDefaultAsync();

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
        public async Task<ActionResult<ReservaDTO>> PostReserva(ReservaDTO reservaDTO, decimal monto, string formatoPago)
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

            var reserva = _mapper.Map<Reserva>(reservaDTO);
            reserva.ClienteId = usuario.Id;

            var pago = new Pago
            {
                ReservaClass = reserva,
                UsuarioClass = await _userManager.GetUserAsync(User),
                FormatoPago = formatoPago,
                Monto = monto
            };

            await _context.Reserva.AddAsync(reserva);
            await _context.Pago.AddAsync(pago);
            await _context.SaveChangesAsync();

            var reservaToReturn = _mapper.Map<ReservaDTO>(reserva);
            return CreatedAtAction(nameof(GetReserva), new { id = reservaToReturn.ReservaId }, reservaToReturn);
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
