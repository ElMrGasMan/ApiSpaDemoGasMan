using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiSpaDemo.Models;
using ApiSpaDemo.Models.DTO;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;

using Microsoft.AspNetCore.Cors;

namespace ApiSpaDemo.Controllers
{
    [EnableCors("PermitirTodo")]
    [Route("api/[controller]")]
    [ApiController]
    public class NoticiaController : ControllerBase
    {
        private readonly ApiSpaDbContext _context;
        private readonly IMapper _mapper;

        public NoticiaController(ApiSpaDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Noticia
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<NoticiaDTO>>> GetNoticia()
        {
            var noticias = await _context.Noticia.ToListAsync();
            var noticiasDTO = _mapper.Map<List<NoticiaDTO>>(noticias);
            return Ok(noticiasDTO);
        }

        // GET: api/Noticia/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<NoticiaDTO>> GetNoticia(int id)
        {
            var noticia = await _context.Noticia.FindAsync(id);

            if (noticia == null)
            {
                return NotFound();
            }

            var noticiaDTO = _mapper.Map<NoticiaDTO>(noticia);
            return Ok(noticiaDTO);
        }

        // POST: api/Noticia
        //[Authorize(Roles = "Admin, Empleado")] SE desactivaron autenticaciones para evitar problemas al probar la api
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<NoticiaDTO>> PostNoticia(NoticiaDTO noticiaDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var noticia = _mapper.Map<Noticium>(noticiaDTO);
            _context.Noticia.Add(noticia);
            await _context.SaveChangesAsync();

            var noticiaToReturn = _mapper.Map<NoticiaDTO>(noticia);
            return CreatedAtAction(nameof(GetNoticia), new { id = noticiaToReturn.NoticiaId }, noticiaToReturn);
        }

        // PUT: api/Noticia/5
        //[Authorize(Roles = "Admin, Empleado")] 
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PutNoticia(int id, NoticiaDTO noticiaDTO)
        {
            if (id != noticiaDTO.NoticiaId)
            {
                return BadRequest();
            }

            var noticia = _mapper.Map<Noticium>(noticiaDTO);
            _context.Entry(noticia).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NoticiaExists(id))
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

        // PATCH: api/Noticia/5
        //[Authorize(Roles = "Admin, Empleado")]
        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PatchNoticia(int id, [FromBody] JsonPatchDocument<NoticiaDTO> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }

            var noticia = await _context.Noticia.FindAsync(id);
            if (noticia == null)
            {
                return NotFound();
            }

            var noticiaDTO = _mapper.Map<NoticiaDTO>(noticia);
            patchDoc.ApplyTo(noticiaDTO, (Microsoft.AspNetCore.JsonPatch.Adapters.IObjectAdapter)ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _mapper.Map(noticiaDTO, noticia);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NoticiaExists(id))
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

        // DELETE: api/Noticia/5
        //[Authorize(Roles = "Admin, Empleado")]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteNoticia(int id)
        {
            var noticia = await _context.Noticia.FindAsync(id);
            if (noticia == null)
            {
                return NotFound();
            }

            _context.Noticia.Remove(noticia);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool NoticiaExists(int id)
        {
            return _context.Noticia.Any(e => e.NoticiaId == id);
        }
    }
}
