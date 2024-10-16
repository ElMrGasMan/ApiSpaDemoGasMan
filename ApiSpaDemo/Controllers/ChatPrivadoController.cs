using ApiSpaDemo.Models;
using ApiSpaDemo.Models.DTO;
using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiSpaDemo.Controllers
{
    [EnableCors("ReglasCors")]
    [Route("api/[controller]")]
    [ApiController]
    public class ChatPrivadoController : ControllerBase
    {
        private readonly ApiSpaDbContext _context;
        private readonly IMapper _mapper;

        public ChatPrivadoController(ApiSpaDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/ChatPrivado/5
        // Obtiene el chat del servicio del usuario autenticado actualmente
        [HttpGet("traerChatAuthUser/{servicioId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ChatPrivadoDTO>> GetChatPrivado(int servicioId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null) return Unauthorized("El usuario no está autenticado.");

            var chatsPrivados = await _context.ChatPrivado
                                   .Where(chat => chat.ServicioId == servicioId && chat.UsuarioId == userId)
                                   .FirstOrDefaultAsync();

            var chatsPrivadosDTO = _mapper.Map<ChatPrivadoDTO>(chatsPrivados);
            return Ok(chatsPrivadosDTO);
        }

        // POST: api/ChatPrivado
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ChatPrivadoDTO>> PostChatPrivado(ChatPrivadoDTO chatPrivadoDTO)
        {
            if (chatPrivadoDTO == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null) return Unauthorized("El usuario no está autenticado.");

            var chatPrivado = _mapper.Map<ChatPrivado>(chatPrivadoDTO);
            chatPrivado.UsuarioId = userId;
            _context.ChatPrivado.Add(chatPrivado);
            await _context.SaveChangesAsync();

            var chatPrivadoToReturn = _mapper.Map<ChatPrivadoDTO>(chatPrivado);
            return CreatedAtAction(nameof(GetChatPrivado), new { id = chatPrivadoToReturn.ChatId }, chatPrivadoToReturn);
        }


        // DELETE: api/ChatPrivado/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteChatPrivado(int id)
        {
            var chatPrivado = await _context.ChatPrivado.FindAsync(id);
            if (chatPrivado == null)
            {
                return NotFound();
            }

            _context.ChatPrivado.Remove(chatPrivado);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
