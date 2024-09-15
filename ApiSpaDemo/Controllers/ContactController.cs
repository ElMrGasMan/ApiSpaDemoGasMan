using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiSpaDemo.Models;
using ApiSpaDemo.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiSpaDemo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactController : ControllerBase
    {
        private readonly EmailService _emailService;

        public ContactController(EmailService emailService)
        {
            _emailService = emailService;
        }


        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("send")]
        public async Task<IActionResult> SendEmail([FromBody] FormContactoModel model)
        {
            if (!ModelState.IsValid) return BadRequest("Datos invalidos");

            await _emailService.SendEmailAsync(model.Email, model.Asunto, model.Mensaje);

            return Ok("Correo enviado correctamente");
        }


        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("sendcv")]
        public async Task<IActionResult> SendEmailWithAttachment([FromForm] FormContactoModel model, IFormFile cv)
        {
            if (cv == null || cv.Length == 0) return BadRequest("Debe adjuntar un archivo.");

            // Leer el archivo en un formato adecuado para el adjunto
            using var memoryStream = new MemoryStream();
            await cv.CopyToAsync(memoryStream);
            var archivoAdjunto = memoryStream.ToArray();
            memoryStream.Close();

            await _emailService.SendEmailCVAsync(
                model.Email,
                model.Asunto,
                model.Mensaje,
                archivoAdjunto,
                cv.FileName
            );

            return Ok("Email enviado con archivo adjunto.");
        }
    }
}