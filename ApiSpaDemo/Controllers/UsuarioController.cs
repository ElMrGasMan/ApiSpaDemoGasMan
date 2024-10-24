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
    [EnableCors("PermitirTodo")]
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
        [HttpPost("cambiarRolUsuario/{userId}")]
        public async Task<IActionResult> CambiarRolUsuario(string userId, string newRole)
        {
            Usuario? user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound($"Usuario con el ID {userId} no encontrado.");
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


        //Elimina un Rol de un usuario.
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost("eliminarRolUsuario/{userId}")]
        public async Task<IActionResult> EliminarRolUsuario(string userId, string newRole)
        {
            Usuario? user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound($"Usuario con el ID {userId} no encontrado.");
            }

            if (!await _roleManager.RoleExistsAsync(newRole))
            {
                return BadRequest("El rol especificado no existe.");
            }

            IList<string> userRoles = await _userManager.GetRolesAsync(user);
            if (!userRoles.Contains(newRole))
            {
                return BadRequest("El usuario no tiene este rol.");
            }

            // Eliminar el rol
            IdentityResult resultAdd = await _userManager.RemoveFromRoleAsync(user, newRole);
            if (resultAdd.Succeeded)
            {
                return Ok("Rol eliminado exitosamente.");
            }

            return StatusCode(StatusCodes.Status500InternalServerError, $"Error al eliminar el rol: {newRole}, del usuario: {userId}.");
        }


        //Cambia el Nombre de Usuario o Email del usuario autenticado
        //Se debe dejar vacio el que no se lo quiere cambiar.
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPatch("cambiarDatosUsuario")]
        public async Task<IActionResult> CambiarDatosUsuarioAuth(string newUsername = "", string newEmail = "")
        {
            if (String.IsNullOrWhiteSpace(newUsername) && String.IsNullOrWhiteSpace(newEmail))
            {
                return BadRequest("Debe proporcionar al menos un nuevo nombre de usuario o email.");
            }

            Usuario? user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized("No hay ningun usuario autenticado actualmente.");
            }

            Usuario? verificacion;
            IdentityResult result;

            //Cambio de Email
            if (!String.IsNullOrWhiteSpace(newEmail))
            {
                verificacion = await _userManager.FindByEmailAsync(newEmail);
                if (verificacion != null && verificacion.Id != user.Id)
                {
                    return BadRequest("El correo electrónico ya está registrado.");
                }

                var emailToken = await _userManager.GenerateChangeEmailTokenAsync(user, newEmail);
                result = await _userManager.ChangeEmailAsync(user, newEmail, emailToken);

                if (!result.Succeeded)
                {
                    return BadRequest($"Error al actualizar el email: {result.Errors.Select(e => e.Description)}.");
                }

                return Ok("Email cambiado exitosamente.");
            }

            //Cambio de Nombre de Usuario
            if (!String.IsNullOrWhiteSpace(newUsername))
            {
                verificacion = await _userManager.FindByNameAsync(newUsername);
                if (verificacion != null && verificacion.Id != user.Id)
                {
                    return BadRequest("Este nombre de usuario ya está en uso.");
                }

                result = await _userManager.SetUserNameAsync(user, newUsername);

                if (!result.Succeeded)
                {
                    return BadRequest($"Error al actualizar el nombre de usuario: {result.Errors.Select(e => e.Description)}.");
                }

                return Ok("Nombre de usuario cambiado exitosamente.");
            }

            return BadRequest("No se realizaron cambios en los datos del usuario.");
        }


        //Cambia el Nombre de Usuario o Email del usuario autenticado
        //Se debe dejar vacio el que no se lo quiere cambiar.
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPatch("cambiarContraseñaUsuario")]
        public async Task<IActionResult> CambiarContraseñaUsuarioAuth(string newUsername = "", string newEmail = "")
        {
            if (String.IsNullOrWhiteSpace(newUsername) && String.IsNullOrWhiteSpace(newEmail))
            {
                return BadRequest("Debe proporcionar al menos un nuevo nombre de usuario o email.");
            }

            Usuario? user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized("No hay ningun usuario autenticado actualmente.");
            }

            Usuario? verificacion;
            IdentityResult result;

            //Cambio de Email
            if (!String.IsNullOrWhiteSpace(newEmail))
            {
                verificacion = await _userManager.FindByEmailAsync(newEmail);
                if (verificacion != null && verificacion.Id != user.Id)
                {
                    return BadRequest("El correo electrónico ya está registrado.");
                }

                var emailToken = await _userManager.GenerateChangeEmailTokenAsync(user, newEmail);
                result = await _userManager.ChangeEmailAsync(user, newEmail, emailToken);

                if (!result.Succeeded)
                {
                    return BadRequest($"Error al actualizar el email: {result.Errors.Select(e => e.Description)}.");
                }

                return Ok("Email cambiado exitosamente.");
            }

            //Cambio de Nombre de Usuario
            if (!String.IsNullOrWhiteSpace(newUsername))
            {
                verificacion = await _userManager.FindByNameAsync(newUsername);
                if (verificacion != null && verificacion.Id != user.Id)
                {
                    return BadRequest("Este nombre de usuario ya está en uso.");
                }

                result = await _userManager.SetUserNameAsync(user, newUsername);

                if (!result.Succeeded)
                {
                    return BadRequest($"Error al actualizar el nombre de usuario: {result.Errors.Select(e => e.Description)}.");
                }

                return Ok("Nombre de usuario cambiado exitosamente.");
            }

            return BadRequest("No se realizaron cambios en los datos del usuario.");
        }


        //Cambia el Nombre de Usuario o Email de un usuario.
        //Se debe dejar vacio el que no se lo quiere cambiar.
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPatch("cambiarDatosUsuario/{userId}")]
        public async Task<IActionResult> CambiarDatosUsuario(string userId, string newUsername = "", string newEmail = "")
        {
            if (String.IsNullOrWhiteSpace(newUsername) && String.IsNullOrWhiteSpace(newEmail))
            {
                return BadRequest("Debe proporcionar al menos un nuevo nombre de usuario o email.");
            }

            Usuario? user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound($"Usuario con el ID {userId} no encontrado.");
            }

            Usuario? verificacion;
            IdentityResult result;

            //Cambio de Email
            if (!String.IsNullOrWhiteSpace(newEmail))
            {
                verificacion = await _userManager.FindByEmailAsync(newEmail);
                if (verificacion != null && verificacion.Id != user.Id)
                {
                    return BadRequest("El correo electrónico ya está registrado.");
                }

                var emailToken = await _userManager.GenerateChangeEmailTokenAsync(user, newEmail);
                result = await _userManager.ChangeEmailAsync(user, newEmail, emailToken);

                if (!result.Succeeded)
                {
                    return BadRequest($"Error al actualizar el email: {result.Errors.Select(e => e.Description)}.");
                }

                return Ok("Email cambiado exitosamente.");
            }

            //Cambio de Nombre de Usuario
            if (!String.IsNullOrWhiteSpace(newUsername))
            {
                verificacion = await _userManager.FindByNameAsync(newUsername);
                if (verificacion != null && verificacion.Id != user.Id)
                {
                    return BadRequest("Este nombre de usuario ya está en uso.");
                }

                result = await _userManager.SetUserNameAsync(user, newUsername);

                if (!result.Succeeded)
                {
                    return BadRequest($"Error al actualizar el nombre de usuario: {result.Errors.Select(e => e.Description)}.");
                }

                return Ok("Nombre de usuario cambiado exitosamente.");
            }

            return BadRequest("No se realizaron cambios en los datos del usuario.");
        }


        //Eliminar un usuario por Id.
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(string id)
        {
            Usuario? usuario = await _userManager.FindByIdAsync(id);

            if (usuario == null)
            return NotFound($"Usuario con el ID: {id} no encontrado.");

            var result = await _userManager.DeleteAsync(usuario);

            if (result == IdentityResult.Success) return NoContent();
            
            else return BadRequest("Error al elmininar el usuario: " + result.Errors);
        }
    }
}