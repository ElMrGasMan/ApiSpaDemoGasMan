using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using ApiSpaDemo.Models;
using ApiSpaDemo.Models.DTO;
using ApiSpaDemo.Models.DTO.PatchDTOs;

using AutoMapper;

using Microsoft.AspNetCore.JsonPatch;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;

namespace ApiSpaDemo.Controllers
{
    [EnableCors("PermitirTodo")]
    [Route("api/[controller]")]
    [ApiController]
    public class ServicioController : ControllerBase
    {
        private readonly ApiSpaDbContext _context;
        private readonly IMapper _mapper;

        public ServicioController(ApiSpaDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Servicio
        // Obtiene todos los servicios
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ServicioDTO>>> GetServicio()
        {
            var servicio = await _context.Servicio.ToListAsync();
            var servicioDTO = _mapper.Map<List<ServicioDTO>>(servicio);
            return servicioDTO;
        }

        // GET: api/Servicio/tipo/{tipo}
        // Obtiene un servicio de un determinado tipo
        [HttpGet("tipo/{tipo}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<ServicioDTO>>> GetServicio([FromQuery] string tipo)
        {
            if (tipo == null)
            {
                return BadRequest();
            }
            var servicios = await _context.Servicio.Where(s => s.TipoServicio == tipo).ToListAsync();

            if (servicios == null || servicios.Count == 0)
            {
                return NotFound();
            }

            var servicioDtos = _mapper.Map<List<ServicioDTO>>(servicios);
            return Ok(servicioDtos);
        }

        // GET: api/Servicio/5
        // Obtiene un servicio en específico, con o sin los Turnos.
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ServicioDTO>> GetServicio(int id, bool conTurnos)
        {
            if (id < 0)
            {
                return BadRequest();
            }

            Servicio? servicio;

            if (conTurnos)
            {
                servicio = await _context.Servicio
                    .Include(s => s.Turnos)
                    .FirstOrDefaultAsync(s => s.ServicioId == id);
            }
            else
            {
                servicio = await _context.Servicio.FindAsync(id);
            }

            if (servicio == null) return NotFound();

            var servicioDTO = _mapper.Map<ServicioDTO>(servicio);
            return servicioDTO;
        }


        // GET: api/Servicio/5
        // Obtiene los servicios de un usuario en especifico, con o sin los Turnos.
        [HttpGet("getServiciosEmpleado/{usuarioId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ServicioDTO>> GetServicioEmpleado(string usuarioId, bool conTurnos)
        {
            if (string.IsNullOrWhiteSpace(usuarioId))
            {
                return BadRequest("No se especifico un ID de usuario.");
            }

            IQueryable<Servicio> query = _context.Servicio.Where(s => s.UsuarioId == usuarioId);

            if (conTurnos) 
                query = _context.Servicio.Include(s => s.Turnos);
            
            var servicios = await query.ToListAsync();

            if (servicios == null || servicios.Count == 0) 
                return NotFound("No se encontró ningun servicio o el empleado no tiene asignado ninguno.");

            var serviciosDTO = _mapper.Map<List<ServicioDTO>>(servicios);
            return Ok(serviciosDTO);
        }


        // PUT: api/Servicio/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[Authorize(Roles = "Admin, Empleado")]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)] 
        public async Task<IActionResult> PutServicio(int id, ServicioDTO servicioDTO)
        {
            if (id != servicioDTO.ServicioId)
            {
                return BadRequest();
            }

            var Servicio = _mapper.Map<Servicio>(servicioDTO);

            _context.Entry(Servicio).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ServicioExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // PATCH: api/Servicio/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[Authorize(Roles = "Admin, Empleado")]
        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]   
        public async Task<IActionResult> PatchServicio(int id, [FromBody] JsonPatchDocument<ServicioPatchDTO> servicioDTOPatch)
        {
            if (servicioDTOPatch == null)
            {
                return BadRequest();
            }

            var servicio = await _context.Servicio.FindAsync(id);

            if (servicio == null)
            {
                return NotFound();
            }

            var servicioDTO = _mapper.Map<ServicioPatchDTO>(servicio);

            servicioDTOPatch.ApplyTo(servicioDTO);
            _mapper.Map(servicioDTO, servicio);
            
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ServicioExists(id)) return NotFound();
                else throw;
            }

            return NoContent();
        }

        // POST: api/Servicio
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[Authorize(Roles = "Admin, Empleado")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ServicioDTO>> PostServicio(ServicioDTO servicioDTO)
        {
            if (servicioDTO == null)
            {
                return BadRequest();
            }
            var servicio = _mapper.Map<Servicio>(servicioDTO);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Servicio.Add(servicio);
            await _context.SaveChangesAsync();

            var servicioToReturn = _mapper.Map<ServicioDTO>(servicio); 

            return CreatedAtAction("GetServicio", new { id = servicioToReturn.ServicioId }, servicioToReturn);
        }

        // DELETE: api/Servicio/5
        //[Authorize(Roles = "Admin, Empleado")]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteServicio(int id)
        {
            var servicio = await _context.Servicio.FindAsync(id);
            if (servicio == null)
            {
                return NotFound();
            }

            _context.Servicio.Remove(servicio);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ServicioExists(int id)
        {
            return _context.Servicio.Any(e => e.ServicioId == id);
        }
    }
}
