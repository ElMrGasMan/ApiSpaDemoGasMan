using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiSpaDemo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ApiSpaDemo.Controllers
{
    [EnableCors("PermitirTodo")]
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly SignInManager<Usuario> _signInManager;

        public AccountController(UserManager<Usuario> userManager, SignInManager<Usuario> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        [EnableCors("PermitirTodo")]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = new Usuario
            {
                UserName = model.Username,
                Email = model.Email
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            // Asignar rol cliente primero
            await _userManager.AddToRoleAsync(user, "Cliente");

            return Ok("User registered successfully.");
        }
        [EnableCors("PermitirTodo")]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            if (!ModelState.IsValid)
            return BadRequest(ModelState);

            // Buscar al usuario por nombre de usuario o email
            var user = await _userManager.FindByNameAsync(model.Username) ?? await _userManager.FindByEmailAsync(model.Username);
            
            if (user == null)
                return Unauthorized("Invalid login attempt.");

            // Verificar la contraseña
            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                // Aquí puedes manejar la creación manual de la cookie de autenticación si es necesario
                await _signInManager.SignInAsync(user, isPersistent: false);
                return Ok("Login successful.");
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
            await _signInManager.SignOutAsync();
            return Ok("Logout successful.");
        }
    }
}