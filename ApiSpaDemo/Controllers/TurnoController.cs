using ApiSpaDemo.Models;
using ApiSpaDemo.Models.DTO;
using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiSpaDemo.Controllers
{
    [EnableCors("ReglasCors")]
    [Route("api/[controller]")]
    [ApiController]
    public class TurnoController : ControllerBase
    {
        private readonly ApiSpaDbContext _context;
        private readonly IMapper _mapper;

        public TurnoController(ApiSpaDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Turno
        // Obtiene todos los turnos
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<TurnoDTO>>> GetTurno()
        {
            var turno = await _context.Turno.ToListAsync();
            var turnoDTO = _mapper.Map<List<TurnoDTO>>(turno);
            return Ok(turnoDTO);
        }


        // GET: api/Turno
        // Obtiene todos los turnos de forma limitada.
        [HttpGet("allTurnosLimited")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<TurnoDTO>>> GetTurnoLimited(int saltear, int tomar)
        {
            var turno = await _context.Turno
                .Skip(saltear)
                .Take(tomar)
                .ToListAsync();
            var turnoDTO = _mapper.Map<List<TurnoDTO>>(turno);
            return Ok(turnoDTO);
        }


        // GET: api/Turno
        // Obtiene todos los turnos de un servicio en específico
        [HttpGet("turnosServicio/{servicioId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<TurnoDTO>>> GetTurnoServ(int servicioId)
        {
            var turno = await _context.Turno
                .Where(t => t.ServicioId == servicioId)
                .ToListAsync();
            var turnoDTO = _mapper.Map<List<TurnoDTO>>(turno);
            return Ok(turnoDTO);
        }


        // POST: api/Turno/5
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TurnoDTO>> PostTurno(TurnoDTO turnoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var turno = _mapper.Map<Turno>(turnoDTO);
            turno.ReservaId = null;
            _context.Turno.Add(turno);
            await _context.SaveChangesAsync();

            var turnoToReturn = _mapper.Map<TurnoDTO>(turno);
            return CreatedAtAction(nameof(GetTurno), new { id = turnoToReturn.IdTurno }, turnoToReturn);
        }

        // DELETE: api/Turno/5
        //[Authorize(Roles = "Admin, Empleado")]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteTurno(int id)
        {
            var turno = await _context.Turno.FindAsync(id);
            if (turno == null)
            {
                return NotFound();
            }

            _context.Turno.Remove(turno);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
