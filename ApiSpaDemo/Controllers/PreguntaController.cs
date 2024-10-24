using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiSpaDemo.Models;
using AutoMapper;
using ApiSpaDemo.Models.DTO;
using Microsoft.AspNetCore.JsonPatch;
using ApiSpaDemo.Models.DTO.PatchDTOs;
using Microsoft.AspNetCore.Cors;
using System.Security.Claims;

namespace ApiSpaDemo.Controllers
{
    [EnableCors("PermitirTodo")]
    [Route("api/[controller]")]
    [ApiController]
    public class PreguntaController : ControllerBase
    {
        private readonly ApiSpaDbContext _context;
        private readonly IMapper _mapper;

        public PreguntaController(ApiSpaDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Pregunta
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<PreguntaDTO>>> GetPregunta()
        {
            var pregunta = await _context.Pregunta.ToListAsync();
            var preguntaDTO = _mapper.Map<List<PreguntaDTO>>(pregunta);
            return preguntaDTO;
        }

        // Limited GET: api/Pregunta/limitado
        [HttpGet("limitado")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<PreguntaDTO>>> GetLimitedpregunta([FromQuery] int count = 10)
        {
            if (count <= 0)
            {
                return BadRequest("El parámetro 'count' debe ser mayor que 0.");
            }

            var pregunta = await _context.Pregunta
                                        .OrderByDescending(n => n.PreguntaId) 
                                        .Take(count)
                                        .ToListAsync();

            var preguntaDTO = _mapper.Map<List<PreguntaDTO>>(pregunta);

            return Ok(preguntaDTO);
        }

        // GET: api/Pregunta/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PreguntaDTO>> GetPregunta(int id)
        {
            var pregunta = await _context.Pregunta.FindAsync(id);
            
            if (id < 0)
            {
                return BadRequest();
            }
            if (pregunta == null)
            {
                return NotFound();
            }

            var preguntaDTO = _mapper.Map<PreguntaDTO>(pregunta);
            return preguntaDTO;
        }

        // PUT: api/Pregunta/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)] 
        public async Task<IActionResult> PutPregunta(int id, PreguntaDTO preguntaDTO)
        {
            if (id != preguntaDTO.PreguntaId)
            {
                return BadRequest();
            }

            var pregunta = _mapper.Map<Pregunta>(preguntaDTO);

            _context.Entry(pregunta).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PreguntaExists(id))
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

        // PATCH: api/Pregunta/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]   
        public async Task<IActionResult> PatchPregunta(int id, [FromBody] JsonPatchDocument<PreguntaPatchDTO> preguntaDTOPatch)
        {
            if (preguntaDTOPatch == null)
            {
                return BadRequest();
            }

            var pregunta = await _context.Pregunta.FindAsync(id);

            if (pregunta == null)
            {
                return NotFound();
            }

            var preguntaDTO = _mapper.Map<PreguntaPatchDTO>(pregunta);

            preguntaDTOPatch.ApplyTo(preguntaDTO);
            _mapper.Map(preguntaDTO, pregunta);
            
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PreguntaExists(id)) return NotFound();
                else throw;
            }

            return NoContent();
        }

        // POST: api/Pregunta
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PreguntaDTO>> PostPregunta(PreguntaDTO preguntaDTO)
        {
            if (preguntaDTO == null)
            {
                return BadRequest();
            }
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            preguntaDTO.UsuarioId = userId;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var pregunta = _mapper.Map<Pregunta>(preguntaDTO);

            _context.Pregunta.Add(pregunta);
            await _context.SaveChangesAsync();

            var preguntaToReturn = _mapper.Map<PreguntaDTO>(pregunta); 

            return CreatedAtAction("GetPregunta", new { id = preguntaToReturn.PreguntaId }, preguntaToReturn);
        }

        // DELETE: api/Pregunta/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeletePregunta(int id)
        {
            var pregunta = await _context.Pregunta.FindAsync(id);
            if (pregunta == null)
            {
                return NotFound();
            }

            _context.Pregunta.Remove(pregunta);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PreguntaExists(int id)
        {
            return _context.Pregunta.Any(e => e.PreguntaId == id);
        }
    }
}
