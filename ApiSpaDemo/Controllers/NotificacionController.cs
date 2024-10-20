using ApiSpaDemo.Models;
using ApiSpaDemo.Models.DTO;
using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ApiSpaDemo.Controllers
{
    [EnableCors("ReglasCors")]
    [Route("api/[controller]")]
    [ApiController]
    public class NotificacionController : ControllerBase
    {
        private readonly ApiSpaDbContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<Usuario> _userManager;

        public NotificacionController(ApiSpaDbContext context, IMapper mapper, UserManager<Usuario> userManager)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
        }

        // GET: api/Notificaciones
        // Obtiene todas las notificaciones
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<NotificacionDTO>>> GetNotificaciones()
        {
            var notificaciones = await _context.Notificacion.ToListAsync();
            var notificacionesDTO = _mapper.Map<List<NotificacionDTO>>(notificaciones);
            return Ok(notificacionesDTO);
        }


        // GET: api/Notificaciones
        // Obtiene todas las notificaciones del usuario actualmente autenticado
        [HttpGet("notificacionesUserAuth")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<IEnumerable<NotificacionDTO>>> GetNotificacionesAuth(bool soloNoLeidos)
        {
            var usuario = await _userManager.GetUserAsync(User);
            if (usuario == null)
            {
                return Unauthorized();
            }

            List<Notificacion> notificaciones = new();

            if (soloNoLeidos)
            {
                notificaciones = await _context.Notificacion
                    .Where(n => n.UsuarioId == usuario.Id && !n.Leido)
                    .ToListAsync();
            }
            else
            {
                notificaciones = await _context.Notificacion
                    .Where(n => n.UsuarioId == usuario.Id)
                    .ToListAsync();
            }

            var notificacionesDTO = _mapper.Map<List<NotificacionDTO>>(notificaciones);
            return Ok(notificacionesDTO);
        }


        // GET: api/Notificaciones
        // Obtiene todas las notificaciones de un usuario específico
        [HttpGet("notificacionesUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<NotificacionDTO>>> GetNotificacionesUsuario(string idUser, bool soloNoLeidos)
        {
            List<Notificacion> notificaciones = new();

            if (soloNoLeidos)
            {
                notificaciones = await _context.Notificacion
                    .Where(n => n.UsuarioId == idUser && !n.Leido)
                    .ToListAsync();
            }
            else
            {
                notificaciones = await _context.Notificacion
                    .Where(n => n.UsuarioId == idUser)
                    .ToListAsync();
            }

            var notificacionesDTO = _mapper.Map<List<NotificacionDTO>>(notificaciones);
            return Ok(notificacionesDTO);
        }


        // POST: api/Notificacion
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        // Si es para el admin entonces no es necesario indicar un Id en el DTO.
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<NotificacionDTO>> PostNotificacion(NotificacionDTO notificacionDTO, bool paraAdmin)
        {
            if (notificacionDTO == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (paraAdmin)
            {
                notificacionDTO.UsuarioId = (await _userManager.GetUsersInRoleAsync("Admin")).First().Id;
            }

            var notificacion = _mapper.Map<Notificacion>(notificacionDTO);

            _context.Notificacion.Add(notificacion);
            await _context.SaveChangesAsync();

            var notificacionToReturn = _mapper.Map<NotificacionDTO>(notificacion);

            return CreatedAtAction("GetNotificaciones", new { id = notificacionToReturn.NotificacionId }, notificacionToReturn);
        }


        // PATCH: api/Notificaciones
        // Marca una notificacion como leida.
        [HttpPatch("leerNotificacion")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> LeerNotificacion(int id)
        {
            Notificacion? notificacion = await _context.Notificacion.FindAsync(id);
            if (notificacion == null)
            {
                return NotFound("Notificacion no encontrada o no existe.");
            }

            notificacion.Leido = true;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error al querer marcar como leida la notificacion: {ex.Message}.");
            }

            return Ok();
        }

        // DELETE: api/Notificacion/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteNotificacion(int id)
        {
            var notificacion = await _context.Notificacion.FindAsync(id);
            if (notificacion == null)
            {
                return NotFound();
            }

            _context.Notificacion.Remove(notificacion);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
