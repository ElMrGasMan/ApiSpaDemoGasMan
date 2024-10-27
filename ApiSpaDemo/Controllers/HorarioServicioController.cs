using ApiSpaDemo.Models;
using ApiSpaDemo.Models.DTO;
using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;

namespace ApiSpaDemo.Controllers
{
    [EnableCors("PermitirTodo")]
    [Route("api/[controller]")]
    [ApiController]
    public class HorarioServicioController : ControllerBase
    {
        private readonly ApiSpaDbContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<Usuario> _userManager;

        public HorarioServicioController(ApiSpaDbContext context, IMapper mapper, UserManager<Usuario> userManager)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
        }


        // GET: api/HorarioServicio
        // Obtiene todos los horarios.
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<HorarioServicioDTO>>> GetHorarioServicios()
        {
            List<HorarioServicio> horariosServicio = await _context.HorarioServicio.ToListAsync();
            List<HorarioServicioDTO> horariosServicioDTO = _mapper.Map<List<HorarioServicioDTO>>(horariosServicio);
            return Ok(horariosServicioDTO);
        }


        // GET: api/HorarioServicio/5
        // Obtiene un horario en especifico.
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<HorarioServicioDTO>>> GetHorarioServicio(int id)
        {
            HorarioServicio? horarioServicio = await _context.HorarioServicio.FindAsync(id);
            if (horarioServicio == null)
            {
                return BadRequest($"No se encontró el HorarioServicio con el ID: {id}.");
            }

            HorarioServicioDTO horarioServicioDTO = _mapper.Map<HorarioServicioDTO>(horarioServicio);
            return Ok(horarioServicioDTO);
        }


        // GET: api/HorarioServicio/deServicio/5
        // Obtiene todos los horarios de un Servicio en específico.
        [HttpGet("getHorariosServicio/{servicioId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<HorarioServicioDTO>>> GetHorariosDeServicio(int servicioId)
        {
            var servicio = await _context.Servicio
                .Include(s => s.Horarios)
                .FirstOrDefaultAsync(s => s.ServicioId == servicioId);

            if (servicio == null)
            {
                return BadRequest($"No se encontro el servicio con el ID {servicioId}.");
            }

            List<HorarioServicio> horariosServicio = servicio.Horarios.ToList();

            var horariosEmpleadoDTO = _mapper.Map<List<HorarioServicioDTO>>(horariosServicio);
            return Ok(horariosEmpleadoDTO);
        }


        // POST: api/HorarioServicio/deEmpleado/5
        // Obtiene todos los horarios de un Empleado en especifico.
        [HttpGet("getHorariosEmpleado/{empleadoId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<HorarioServicioDTO>>> GetHorariosDeEmpleado(string empleadoId)
        {
            var empleado = await _userManager.FindByIdAsync(empleadoId);
            if (empleado == null)
            {
                return BadRequest($"No se encontro el empleado con el ID {empleadoId}.");
            }

            var servicios = await _context.Servicio
                .Where(s => s.UsuarioId == empleado.Id)
                .Include(s => s.Horarios)
                .ToListAsync();

            List<HorarioServicio> horariosEmpleado = [];

            foreach (var servicio in servicios)
            {
                horariosEmpleado.AddRange(servicio.Horarios);
            }

            var horariosEmpleadoDTO = _mapper.Map<HorarioServicioDTO>(horariosEmpleado);
            return Ok(horariosEmpleadoDTO);
        }


        // POST: api/HorarioServicio
        // Crea un nuevo Horario.
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<HorarioServicioDTO>> PostHorarioServicio(HorarioServicioDTO horarioServicioDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var horarioServicio = _mapper.Map<HorarioServicio>(horarioServicioDTO);
            _context.HorarioServicio.Add(horarioServicio); 
            await _context.SaveChangesAsync();

            var horarioServicioToReturn = _mapper.Map<HorarioServicioDTO>(horarioServicio);
            return CreatedAtAction(nameof(GetHorarioServicio), new { id = horarioServicioToReturn.HorarioServicioId }, horarioServicioToReturn);
        }


        // PATCH: api/HorarioServicio/agregarAServicio/5
        // Asigna un horario a un servicio.
        [HttpPatch("asignarHorarioAServicio/{servicioId}, {horarioId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AsignarHorarioAServicio(int servicioId, int horarioId)
        {
            var servicio = await _context.Servicio
                .Include(s => s.Horarios)
                .FirstOrDefaultAsync(s => s.ServicioId == servicioId);
            if (servicio == null)
            {
                return BadRequest($"No se encontro el servicio con el ID {servicioId}.");
            }

            var horarioServicio = await _context.HorarioServicio.FindAsync(horarioId);
            if (horarioServicio == null)
            {
                return BadRequest($"No se encontro el horario con el ID {horarioId}.");
            }
            if (horarioServicio.ServicioClass != null)
            {
                return BadRequest($"Este horario de ID: {horarioId}, ya está asignado a otro servicio.");
            }

            horarioServicio.ServicioId = servicio.ServicioId;
            servicio.Horarios.Add(horarioServicio);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    $"Error al tratar de agregar el horario con ID: {horarioId}, al servicio con ID: {servicioId}. {ex.Message}");
            }

            return Ok($"El horario con el ID: {horarioId}, fue asignado correctamente al servicio con ID: {servicioId}.");
        }


        // PATCH: api/HorarioServicio/agregarAServicio/5
        // Elimina un horario de un servicio.
        [HttpPatch("eliminarHorarioDeServicio/{servicioId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> EliminarHorarioDeServicio(int servicioId, int horarioId)
        {
            var servicio = await _context.Servicio
                .Include(s => s.Horarios)
                .FirstOrDefaultAsync(s => s.ServicioId == servicioId);
            if (servicio == null)
            {
                return BadRequest($"No se encontro el servicio con el ID {servicioId}.");
            }

            var horarioServicio = await _context.HorarioServicio.FindAsync(horarioId);
            if (horarioServicio == null)
            {
                return BadRequest($"No se encontro el horario con el ID {horarioId}.");
            }
            if (horarioServicio.ServicioClass == null)
            {
                return BadRequest($"El horario con ID: {horarioId}, no esta asignado a ningun servicio.");
            }

            servicio.Horarios.Remove(horarioServicio);
            horarioServicio.ServicioId = null;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error al tratar de eliminar el horario con ID: {horarioId}, del servicio con ID: {servicioId}. {ex.Message}");
            }

            return Ok($"El horario con el ID: {horarioId}, fue eliminado correctamente del servicio con ID: {servicioId}.");
        }


        // DELETE: api/HorarioServicio/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteHorarioServicio(int id)
        {
            HorarioServicio? horarioServicio = await _context.HorarioServicio.FindAsync(id);
            if (horarioServicio == null)
            {
                return NotFound();
            }

            _context.HorarioServicio.Remove(horarioServicio);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
