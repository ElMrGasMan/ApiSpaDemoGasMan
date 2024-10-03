using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using ApiSpaDemo.Models;
using ApiSpaDemo.Models.DTO;
using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiSpaDemo.Controllers
{
    [EnableCors("ReglasCors")]
    [ApiController]
    [Route("api/[controller]")]
    public class MensajePrivadoController : ControllerBase
    {
        private readonly ApiSpaDbContext _context;
        private readonly IMapper _mapper;

        public MensajePrivadoController(ApiSpaDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/MensajePrivado
        // Obtiene todos los mensajes (creo que no hace falta)
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<MensajePrivadoDTO>>> GetMensajePrivado()
        {
            var mensajesPrivados = await _context.MensajePrivado.ToListAsync();
            var mensajesPrivadosDTO = _mapper.Map<List<MensajePrivadoDTO>>(mensajesPrivados);
            return Ok(mensajesPrivadosDTO);
        }

        // GET: api/MensajePrivado/5
        // Obtiene todos los mensajes de un cierto chat 
        [HttpGet("{chatId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<MensajePrivadoDTO>> GetMensajePrivado(int chatId)
        {
            var mensajesPrivados = await _context.MensajePrivado
                                   .Where(men => men.ChatId == chatId)
                                   .ToListAsync();

            var mensajesPrivadosDTO = _mapper.Map<List<MensajePrivadoDTO>>(mensajesPrivados);
            return Ok(mensajesPrivadosDTO);
        }


        // POST: api/MensajePrivado
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<MensajePrivadoDTO>> PostMensajePrivado(MensajePrivadoDTO MensajePrivadoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null) return Unauthorized("El usuario no está autenticado.");

            var mensajePrivado = _mapper.Map<MensajePrivado>(MensajePrivadoDTO);
            mensajePrivado.UsuarioId = userId;
            _context.MensajePrivado.Add(mensajePrivado);
            await _context.SaveChangesAsync();

            var mensajePrivadoToReturn = _mapper.Map<MensajePrivadoDTO>(mensajePrivado);
            return CreatedAtAction(nameof(GetMensajePrivado), new { id = mensajePrivadoToReturn.MensajePrivadoId }, mensajePrivadoToReturn);
        }


        // DELETE: api/MensajePrivado/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteMensajePrivado(int id)
        {
            var mensajePrivado = await _context.MensajePrivado.FindAsync(id);
            if (mensajePrivado == null)
            {
                return NotFound();
            }

            _context.MensajePrivado.Remove(mensajePrivado);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
