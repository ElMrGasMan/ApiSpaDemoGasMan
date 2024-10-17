using System.Security.Claims;
using ApiSpaDemo.Models;
using ApiSpaDemo.Models.DTO;
using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiSpaDemo.Controllers
{
    [EnableCors("ReglasCors")]
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly ApiSpaDbContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<Usuario> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UsuarioController(ApiSpaDbContext context, IMapper mapper, UserManager<Usuario> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        //Obtener un usuario por Id.
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioDTOwID>> GetUsuario(string id)
        {
            Usuario? usuario = await _userManager.FindByIdAsync(id);

            if (usuario == null)
            {
                return NotFound();
            }

            UsuarioDTOwID usuarioDto = _mapper.Map<UsuarioDTOwID>(usuario);
            usuarioDto.Roles = (await _userManager.GetRolesAsync(usuario)).ToList();
            return Ok(usuarioDto);
        }

        //Obtiene datos del usuario autenticado actualmente
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet("currentuserdata")]
        public async Task<ActionResult<UsuarioDTOwID>> GetUsuario()
        {
            Usuario? usuario = await _userManager.GetUserAsync(User);
            if (usuario == null)
            {
                return Unauthorized("No hay ningun usuario autenticado actualmente.");
            }

            string userId = usuario.Id;
            string? userName = usuario.UserName;
            string? email = usuario.Email;
            IList<string> roles = await _userManager.GetRolesAsync(usuario);

            UsuarioDTOwID usuarioDto = new UsuarioDTOwID
            {
                Id = userId,
                UserName = userName,
                Email = email,
                Roles = roles.ToList()
            };
            return Ok(usuarioDto);
        }


        //Obtener todos los usuarios.
        //Opcionalmente se puede pasar para filtrar por Rol.
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("getAllUsers")]
        public async Task<ActionResult<IEnumerable<UsuarioDTOwID>>> GetAllUsuarios()
        {
            List<Usuario> usuarios = await _userManager.Users.ToListAsync();
            List<UsuarioDTOwID> usuariosConRoles = new List<UsuarioDTOwID>();

            foreach (Usuario? usuario in usuarios)
            {
                IList<string> roles = await _userManager.GetRolesAsync(usuario);

                usuariosConRoles.Add(new UsuarioDTOwID
                {
                    Id = usuario.Id,
                    UserName = usuario.UserName,
                    Email = usuario.Email,
                    Roles = roles.ToList()
                });
            }

            return Ok(usuariosConRoles);
        }

        //Obtener los usuarios filtrados por un Rol
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("getAllUsersByRol/{rol}")]
        public async Task<ActionResult<IEnumerable<UsuarioDTOwID>>> GetAllUsuariosPorRol(string rol)
        {
            IList<Usuario> usuarios = await _userManager.GetUsersInRoleAsync(rol);
            List<UsuarioDTOwID> usuariosConRoles = new List<UsuarioDTOwID>();

            foreach (Usuario usuario in usuarios)
            {
                IList<string> roles = await _userManager.GetRolesAsync(usuario);

                usuariosConRoles.Add(new UsuarioDTOwID
                {
                    Id = usuario.Id,
                    UserName = usuario.UserName,
                    Email = usuario.Email,
                    Roles = roles.ToList()
                });
            }

            return Ok(usuariosConRoles);
        }


        //Cambia el rol de un usuario.
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost("cambiarRolUsuario")]
        public async Task<IActionResult> CambiarRolUsuario(string userId, string newRole)
        {
            Usuario? user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound("Usuario no encontrado");
            }

            if (!await _roleManager.RoleExistsAsync(newRole))
            {
                return BadRequest("El rol especificado no existe");
            }

            IList<string> userRoles = await _userManager.GetRolesAsync(user);
            if (userRoles.Contains(newRole))
            {
                return BadRequest("El usuario ya tiene este rol");
            }

            // Asignar el nuevo rol
            IdentityResult resultAdd = await _userManager.AddToRoleAsync(user, newRole);
            if (resultAdd.Succeeded)
            {
                return Ok("Rol cambiado exitosamente");
            }

            return StatusCode(StatusCodes.Status500InternalServerError, "Error al asignar el nuevo rol");
        }
    }
}