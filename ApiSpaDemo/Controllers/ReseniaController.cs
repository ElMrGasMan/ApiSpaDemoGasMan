using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiSpaDemo.Models;

using AutoMapper;
using ApiSpaDemo.Models.DTO;
using Microsoft.AspNetCore.JsonPatch;
using ApiSpaDemo.Models.DTO.PatchDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;

namespace ApiSpaDemo.Controllers
{
    [EnableCors("ReglasCors")]
    [Route("api/[controller]")]
    [ApiController]
    public class ReseniaController : ControllerBase
    {
        private readonly ApiSpaDbContext _context;
        private readonly IMapper _mapper;

        public ReseniaController(ApiSpaDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Resenia
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ReseniaDTO>>> GetResenia()
        {
            var resenia = await _context.Resenia.ToListAsync();
            var reseniaDTO = _mapper.Map<List<ReseniaDTO>>(resenia);
            return reseniaDTO;
        }

        // Limited GET: api/Resenia/limitado
        [HttpGet("limitado")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<ReseniaDTO>>> GetLimitedResenias([FromQuery] int count = 10)
        {
            if (count <= 0)
            {
                return BadRequest("El parámetro 'count' debe ser mayor que 0.");
            }

            var resenias = await _context.Resenia
                                        .OrderByDescending(n => n.ReseniaId) 
                                        .Take(count)
                                        .ToListAsync();

            var reseniasDTO = _mapper.Map<List<ReseniaDTO>>(resenias);

            return Ok(reseniasDTO);
        }

        // GET: api/Resenia/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ReseniaDTO>> GetResenia(int id)
        {
            var resenia = await _context.Resenia.FindAsync(id);
            
            if (id < 0)
            {
                return BadRequest();
            }
            if (resenia == null)
            {
                return NotFound();
            }

            var reseniaDTO = _mapper.Map<ReseniaDTO>(resenia);
            return reseniaDTO;
        }

        // PUT: api/Resenia/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[Authorize]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)] 
        public async Task<IActionResult> PutResenia(int id, ReseniaDTO reseniaDTO)
        {
            if (id != reseniaDTO.ReseniaId)
            {
                return BadRequest();
            }

            var resenia = _mapper.Map<Resenia>(reseniaDTO);

            _context.Entry(resenia).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReseniaExists(id))
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

        // PATCH: api/Resenia/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[Authorize]
        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]   
        public async Task<IActionResult> PatchResenia(int id, [FromBody] JsonPatchDocument<ReseniaPatchDTO> reseniaDTOPatch)
        {
            if (reseniaDTOPatch == null)
            {
                return BadRequest();
            }

            var resenia = await _context.Resenia.FindAsync(id);

            if (resenia == null)
            {
                return NotFound();
            }

            var reseniaDTO = _mapper.Map<ReseniaPatchDTO>(resenia);

            reseniaDTOPatch.ApplyTo(reseniaDTO);
            _mapper.Map(reseniaDTO, resenia);
            
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReseniaExists(id)) return NotFound();
                else throw;
            }

            return NoContent();
        }

        // POST: api/Resenia
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ReseniaDTO>> PostResenia(ReseniaDTO reseniaDTO)
        {
            if (reseniaDTO == null)
            {
                return BadRequest();
            }
            var resenia = _mapper.Map<Resenia>(reseniaDTO);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Resenia.Add(resenia);
            await _context.SaveChangesAsync();

            var reseniaToReturn = _mapper.Map<ReseniaDTO>(resenia); 

            return CreatedAtAction("GetResenia", new { id = reseniaToReturn.ReseniaId }, reseniaToReturn);
        }

        // DELETE: api/Resenia/5
        //[Authorize]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteResenia(int id)
        {
            var resenia = await _context.Resenia.FindAsync(id);
            if (resenia == null)
            {
                return NotFound();
            }

            _context.Resenia.Remove(resenia);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ReseniaExists(int id)
        {
            return _context.Resenia.Any(e => e.ReseniaId == id);
        }
    }
}
