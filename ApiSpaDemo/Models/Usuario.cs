using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace ApiSpaDemo.Models
{
    public class Usuario : IdentityUser
    {
        [Required]
        public ICollection<Servicio> Servicios { get; set; } = []; 
        [Required]
        public ICollection<Resenia> Resenias { get; set; } = []; 
        [Required]
        public ICollection<Pregunta> Preguntas { get; set; } = []; 
        [Required]
        public ICollection<Respuesta> Respuestas { get; set; } = [];

        // Relación con ChatPrivado (muchos a muchos)
        public ICollection<ChatPrivado> ChatsPrivados { get; set; }

        public ICollection<MensajePrivado> MensajesPrivados { get; set; }
    }
}