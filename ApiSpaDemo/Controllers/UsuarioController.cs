using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ApiSpaDemo.Models;
using ApiSpaDemo.Models.DTO;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiSpaDemo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly ApiSpaDbContext _context;
        private readonly IMapper _mapper;

        public UsuarioController(ApiSpaDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
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

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("currentuserdata")]
        public async Task<ActionResult<UsuarioDTO>> GetUsuario()
        {
            var userName = User.FindFirst(ClaimTypes.Name)?.Value;
            var email = User.FindFirst(ClaimTypes.Email)?.Value;

            var usuarioDto = new UsuarioDTO
            {
                UserName = userName,
                Email = email
            };
            return Ok(usuarioDto);
        }

        //Solo para testing, obtener todos los usuarios.
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("getAllUsers")]
        public async Task<ActionResult<IEnumerable<UsuarioDTOwID>>> GetAllUsuarios()
        {
            var usuarios = await _context.Users.ToListAsync();
            var usuarioDTOs = _mapper.Map<List<UsuarioDTOwID>>(usuarios);
            return usuarioDTOs;
        }
    }
}