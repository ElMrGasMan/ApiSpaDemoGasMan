using ApiSpaDemo.Models;
using ApiSpaDemo.Models.DTO;
using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiSpaDemo.Controllers
{
    [EnableCors("PermitirTodo")]
    [Route("api/[controller]")]
    [ApiController]
    public class ListadosController : ControllerBase
    {
        private readonly ApiSpaDbContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<Usuario> _userManager;

        public ListadosController(ApiSpaDbContext context, IMapper mapper, UserManager<Usuario> userManager)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
        }


        // GET: api/ListadosController
        // Obtiene una lista con todos los clientes que hay que atender por dia.
        [HttpGet("clientesPorDia")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<RegistroClientePorDia>>> GetListadoClientesPorDia(DateOnly dia, bool ordenarDescendiente)
        {
            // Traer solo los turnos que sean del dia especificado y estén pagados
            var query = _context.Turno
                .Where(t => DateOnly.FromDateTime(t.FechaInicio) == dia && t.ReservaClass != null && t.ServicioClass != null && t.ReservaClass.Pago.Pagado == true)
                .Select(t => new RegistroClientePorDia
                {
                    ClienteId = t.ReservaClass.ClienteId,
                    ClienteUsername = t.ReservaClass.Cliente.UserName ?? "N/A",
                    TituloServicioARealizar = t.ServicioClass.Titulo ?? "N/A",
                    Horario = t.FechaInicio
                });

            var listaClientes = ordenarDescendiente
                ? await query.OrderByDescending(registro => registro.Horario).ToListAsync()
                : await query.OrderBy(registro => registro.Horario).ToListAsync();

            return Ok(listaClientes);
        }


        // GET: api/ListadosController
        // Obtiene todos los clientes que cada empleado debe atender, ordenado por horario.
        [HttpGet("clientesPorEmpleado")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<RegistroClientePorEmpleado>>> GetListadoClientesPorEmpleado(bool ordenarDescendiente)
        {
            // Traer todos los turnos pagados, mas todas las cosas necesarias.
            // Ademas no traer los turnos que sean anteriores al dia de hoy.
            var query = _context.Turno
                .Where(t => t.ReservaClass != null && t.ServicioClass != null && t.ServicioClass.UsuarioClass != null && t.ReservaClass.Pago.Pagado == true && t.FechaInicio >= DateTime.Now)
                .Select(t => new RegistroClientePorEmpleado
                {
                    ClienteId = t.ReservaClass.ClienteId,
                    ClienteUsername = t.ReservaClass.Cliente.UserName ?? "N/A",
                    EmpleadoUsername = t.ServicioClass.UsuarioClass.UserName ?? "N/A",
                    Horario = t.FechaInicio
                });

            var listaClientes = ordenarDescendiente
                ? await query.OrderByDescending(registro => registro.Horario).ToListAsync()
                : await query.OrderBy(registro => registro.Horario).ToListAsync();

            return Ok(listaClientes);
        }

        public class RegistroClientePorDia 
        {
            public string ClienteId { get; set; } = "";
            public string ClienteUsername { get; set; } = "";
            public string TituloServicioARealizar { get; set; } = "";
            public DateTime Horario { get; set; } = new();
        }

        public class RegistroClientePorEmpleado
        {
            public string ClienteId { get; set; } = "";
            public string ClienteUsername { get; set; } = "";
            public string EmpleadoUsername { get; set; } = "";
            public DateTime Horario { get; set; } = new();
        }
    }
}
