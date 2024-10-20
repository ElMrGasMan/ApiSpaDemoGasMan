using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiSpaDemo.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ApiSpaDemo.Controllers
{
    [EnableCors("PermitirTodo")]
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly SignInManager<Usuario> _signInManager;
        private readonly ILogger<AccountController> _logger;

        public AccountController(UserManager<Usuario> userManager, SignInManager<Usuario> signInManager, ILogger<AccountController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _logger = logger;
        }
        [EnableCors("PermitirTodo")]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                Usuario user = new Usuario
                {
                    UserName = model.Username,
                    Email = model.Email
                };

                IdentityResult result = await _userManager.CreateAsync(user, model.Password);

                if (!result.Succeeded)
                    return BadRequest(result.Errors);

                // Asignar rol cliente primero
                await _userManager.AddToRoleAsync(user, "Cliente");

                return Ok("User registered successfully.");
            }catch (Exception ex) {
                _logger.LogError(ex, "Error en el registro de usuario");
                return StatusCode(500, new { message = "Internal server error", error = ex.Message });
            }
        }


        [EnableCors("PermitirTodo")]
        [HttpPost("registerEmpleado")]
        public async Task<IActionResult> RegisterEmpleado([FromBody] RegisterModel model, bool esSecretario)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                Usuario user = new Usuario
                {
                    UserName = model.Username,
                    Email = model.Email
                };

                IdentityResult result = await _userManager.CreateAsync(user, model.Password);

                if (!result.Succeeded)
                    return BadRequest(result.Errors);

                if (esSecretario)
                {
                    result = await _userManager.AddToRoleAsync(user, "Secretario");
                }
                else
                {
                    result = await _userManager.AddToRoleAsync(user, "Empleado");
                }

                if (!result.Succeeded) return BadRequest(result.Errors);
                
                return Ok("Empleado o Secretario registrado correctamente.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en el registro de usuario");
                return StatusCode(500, new { message = "Internal server error", error = ex.Message });
            }
        }


        [EnableCors("PermitirTodo")]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            if (!ModelState.IsValid)
            return BadRequest(ModelState);

            // Buscar al usuario por nombre de usuario o email
            Usuario? user = await _userManager.FindByNameAsync(model.Username) ?? await _userManager.FindByEmailAsync(model.Username);
            
            if (user == null)
                return Unauthorized("Invalid login attempt.");

            // Verificar la contraseña
            Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                // Aquí puedes manejar la creación manual de la cookie de autenticación si es necesario
                await _signInManager.SignInAsync(user, isPersistent: false);

                return Ok(new { 
                    message = "Login successful.",
                    userName = user.UserName, 
                    email = user.Email, 
                    idUser = user.Id,
                });
            }

            if (result.IsLockedOut)
            {
                return Unauthorized("This account has been locked out due to multiple failed login attempts.");
            }

            return Unauthorized("Invalid login attempt.");
        }
        [EnableCors("PermitirTodo")]
        [HttpPost("logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {

            try
            {
                // Esto cerrará la sesión del usuario y eliminará las cookies de autenticación
                await _signInManager.SignOutAsync();
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

                return Ok("Logout successful.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en el logout");
                return StatusCode(500, new { message = "Internal server error", error = ex.Message });
            }
        }
    }
}