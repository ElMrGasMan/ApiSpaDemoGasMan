using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
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

        //Obtener un usuario por Id y devolver solo lo necesario.
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioDTO>> GetUsuario(string id)
        {
            var usuario = await _context.Users.FirstOrDefaultAsync(u => u.Id.ToLower() == id.ToLower());

            if (usuario == null)
            {
                return NotFound();
            }

            var usuarioDto = _mapper.Map<UsuarioDTO>(usuario);
            return Ok(usuarioDto);
        }

        //Obtiene datos del usuario autenticado actualmente
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("currentuserdata")]
        public async Task<ActionResult<UsuarioDTOwID>> GetUsuario()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var userName = User.FindFirst(ClaimTypes.Name)?.Value;
            var email = User.FindFirst(ClaimTypes.Email)?.Value;

            var usuarioDto = new UsuarioDTOwID
            {
                Id = userId,
                UserName = userName,
                Email = email
            };
            return Ok(usuarioDto);
        }


        //Obtener todos los usuarios.
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("getAllUsers")]
        public async Task<ActionResult<IEnumerable<UsuarioDTOwID>>> GetAllUsuarios()
        {
            var usuarios = await _context.Users.ToListAsync();
            var usuarioDTOs = _mapper.Map<List<UsuarioDTOwID>>(usuarios);
            return usuarioDTOs;
        }


        //Obtener todos los usuarios. De forma limitada
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("getAllUsersLimited")]
        public async Task<ActionResult<IEnumerable<UsuarioDTOwID>>> GetAllUsuariosLimited(int saltear, int tomar)
        {
            var usuarios = await _context.Users
                .Skip(saltear)
                .Take(tomar)
                .ToListAsync();
            var usuarioDTOs = _mapper.Map<List<UsuarioDTOwID>>(usuarios);
            return usuarioDTOs;
        }


        //Cambia el rol de un usuario.
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost("cambiarRolUsuario")]
        public async Task<IActionResult> CambiarRolUsuario(string userId, string newRole)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound("Usuario no encontrado");
            }

            if (!await _roleManager.RoleExistsAsync(newRole))
            {
                return BadRequest("El rol especificado no existe");
            }

            var userRoles = await _userManager.GetRolesAsync(user);
            if (userRoles.Contains(newRole))
            {
                return BadRequest("El usuario ya tiene este rol");
            }

            // Asignar el nuevo rol
            var resultAdd = await _userManager.AddToRoleAsync(user, newRole);
            if (resultAdd.Succeeded)
            {
                return Ok("Rol cambiado exitosamente");
            }

            return StatusCode(StatusCodes.Status500InternalServerError, "Error al asignar el nuevo rol");
        }
    }
}