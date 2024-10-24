using ApiSpaDemo.Models;
using ApiSpaDemo.Models.DTO;
using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiSpaDemo.Controllers
{
    [EnableCors("PermitirTodo")]
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
            List<Turno> turno = await _context.Turno.ToListAsync();
            List<TurnoDTO> turnoDTO = _mapper.Map<List<TurnoDTO>>(turno);
            return Ok(turnoDTO);
        }

        /*
        // GET: api/Turno
        // Obtiene todos los turnos de forma limitada.
        [HttpGet("allTurnosLimited")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<TurnoDTO>>> GetTurnoLimited(int saltear, int tomar)
        {
            List<Turno> turno = await _context.Turno
                .Skip(saltear)
                .Take(tomar)
                .ToListAsync();
            List<TurnoDTO> turnoDTO = _mapper.Map<List<TurnoDTO>>(turno);
            return Ok(turnoDTO);
        }
        */


        // PATCH: api/Turno
        // Cambia la descripcion de un turno en especifico
        [HttpPatch("cambiarDescripcion/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<TurnoDTO>>> GetTurnoServ(int id, string nuevaDescripcion)
        {
            Turno? turno = await _context.Turno.FindAsync(id);
            if (turno == null)
            {
                return BadRequest($"No se encontró un Turno con el ID: {id}.");
            }

            turno.Descripcion = nuevaDescripcion;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error al actualizar el Turno: {ex.Message}");
            }

            return NoContent();
        }


        // GET: api/Turno
        // Obtiene todos los turnos de un servicio en específico
        [HttpGet("turnosServicio/{servicioId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<TurnoDTO>>> GetTurnoServ(int servicioId)
        {
            List<Turno> turno = await _context.Turno
                .Where(t => t.ServicioId == servicioId)
                .ToListAsync();
            List<TurnoDTO> turnoDTO = _mapper.Map<List<TurnoDTO>>(turno);
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

            Turno turno = _mapper.Map<Turno>(turnoDTO);
            turno.ReservaId = null;
            _context.Turno.Add(turno);
            await _context.SaveChangesAsync();

            TurnoDTO turnoToReturn = _mapper.Map<TurnoDTO>(turno);
            return CreatedAtAction(nameof(GetTurno), new { id = turnoToReturn.TurnoId }, turnoToReturn);
        }

        // DELETE: api/Turno/5
        //[Authorize(Roles = "Admin, Empleado")]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteTurno(int id)
        {
            Turno? turno = await _context.Turno.FindAsync(id);
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
