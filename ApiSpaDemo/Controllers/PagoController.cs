using ApiSpaDemo.Models;
using ApiSpaDemo.Models.DTO;
using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ApiSpaDemo.Controllers
{
    [EnableCors("ReglasCors")]
    [Route("api/[controller]")]
    [ApiController]
    public class PagoController : ControllerBase
    {
        private readonly ApiSpaDbContext _context;
        private readonly IMapper _mapper;

        public PagoController(ApiSpaDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        // GET: api/Pago
        // Obtiene todos los pagos de todos los usuarios
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<PagoDTO>>> GetPagos()
        {
            var pagos = await _context.Pago.ToListAsync();
            var pagosDTO = _mapper.Map<List<PagoDTO>>(pagos);
            return Ok(pagosDTO);
        }


        // GET: api/Pago/5
        // Obtiene todos los pagos de un cierto usuario
        [HttpGet("{usuarioId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<PagoDTO>>> GetPagosUsuario(string usuarioId)
        {
            var pagos = await _context.Pago
                                   .Where(pag => pag.UsuarioId == usuarioId)
                                   .ToListAsync();
            var pagosDTO = _mapper.Map<List<PagoDTO>>(pagos);
            return Ok(pagosDTO);
        }


        // DELETE: api/Pago/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeletePago(int id)
        {
            var pago = await _context.Pago.FindAsync(id);
            if (pago == null)
            {
                return NotFound();
            }

            _context.Pago.Remove(pago);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
