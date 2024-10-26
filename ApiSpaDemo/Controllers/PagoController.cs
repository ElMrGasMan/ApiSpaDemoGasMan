using ApiSpaDemo.Models;
using ApiSpaDemo.Models.DTO;
using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Pqc.Crypto.Utilities;
using System.Security.Claims;

namespace ApiSpaDemo.Controllers
{
    [EnableCors("PermitirTodo")]
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
            List<Pago> pagos = await _context.Pago.ToListAsync();
            List<PagoDTO> pagosDTO = _mapper.Map<List<PagoDTO>>(pagos);
            return Ok(pagosDTO);
        }

        /*
        // GET: api/Pago
        // Obtiene todos los pagos de todos los usuarios de forma Limitada
        [HttpGet("getAllPagosLimited")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<PagoDTO>>> GetPagosLimited(int saltear, int tomar)
        {
            var pagos = await _context.Pago
                .Skip(saltear)
                .Take(tomar)
                .ToListAsync();
            var pagosDTO = _mapper.Map<List<PagoDTO>>(pagos);
            return Ok(pagosDTO);
        }
        */


        // GET: api/Pago/5
        // Obtiene todos los pagos de un cierto usuario
        [HttpGet("{usuarioId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<PagoDTO>>> GetPagosUsuario(string usuarioId)
        {
            List<Pago> pagos = await _context.Pago
                                   .Where(pag => pag.UsuarioId == usuarioId)
                                   .ToListAsync();
            List<PagoDTO> pagosDTO = _mapper.Map<List<PagoDTO>>(pagos);
            return Ok(pagosDTO);
        }


        // GET: api/Pago/getPago/5
        // Obtiene un pago en especifico
        [HttpGet("getPago/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<PagoDTO>>> GetPago(int id)
        {
            Pago? pago = await _context.Pago.FindAsync(id);
            if (pago == null)
            {
                return BadRequest($"No se encontró el Pago con el ID: {id}.");
            }

            PagoDTO pagoDTO = _mapper.Map<PagoDTO>(pago);
            return Ok(pagoDTO);
        }

        /*
        // PATCH: api/Pago/5
        // Suma o Resta al monto total del Pago: 1 para sumar, 0 para restar.
        // MUY IMPORTANTE: ACORDARSE DE LLAMAR ESTE ENDPOINT A LA HORA DE QUE SE ELIMINE O AGREGUE UN 
        // TURNO EN EL FRONT-END.
        [HttpPatch("actualizarMonto/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<PagoDTO>>> SumarRestarPago(int id, short action, decimal monto)
        {
            Pago? pago = await _context.Pago.FindAsync(id);
            if (pago == null)
            {
                return NotFound();
            }

            switch (action)
            {
                case 1: //Suma
                    pago.MontoTotal += monto;
                    break;

                case 0: //Resta
                    if (pago.MontoTotal - monto < 0) return BadRequest("Restar este monto dejaria en negativo el pago total.");
                    pago.MontoTotal -= monto;
                    break;

                default:
                    return BadRequest("Operación inválida.");
            }

            // Guardar los cambios en la base de datos
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error al actualizar el pago: {ex.Message}");
            }

            return Ok($"El monto total del pago con ID {id} se ha actualizado a {pago.MontoTotal}.");
        }
        */

        
        // PATCH: api/Pago/5
        // Establece el Pago como Pagado.
        [HttpPatch("establecerPagado/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<PagoDTO>>> EstablecerEstadoPago(int id)
        {
            Pago? pago = await _context.Pago.FindAsync(id);
            if (pago == null) return NotFound($"El Pago con ID: {id}, no fue encontrado.");

            if (pago.Pagado == true) return BadRequest($"Este Pago con ID: {id}, ya fue pagado anteriormente.");

            pago.Pagado = true;
            pago.FechaPagado = DateTime.Now;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error al actualizar el pago: {ex.Message}");
            }

            return Ok($"El estado del pago con ID: {id}, se ha indicado como pagado.");
        }


        // PATCH: api/Pago/5
        // Establece el Metodo o Formato de Pago.
        [HttpPatch("establecerMetodoPago/{metodoPago}, {id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<PagoDTO>>> EstablecerMetodoPago(string metodoPago, int id)
        {
            Pago? pago = await _context.Pago.FindAsync(id);
            if (pago == null) return NotFound($"El Pago con ID: {id}, no fue encontrado.");

            if (pago.Pagado == true) return BadRequest($"Este Pago con ID: {id}, ya fue pagado anteriormente.");

            pago.FormatoPago = metodoPago;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error al actualizar el pago: {ex.Message}");
            }

            return Ok($"El estado del pago con ID: {id}, se ha indicado como pagado.");
        }


        // DELETE: api/Pago/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeletePago(int id)
        {
            Pago? pago = await _context.Pago.FindAsync(id);
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
