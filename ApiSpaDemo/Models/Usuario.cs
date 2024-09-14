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
        [Required]
        public ICollection<Turno> Turnos { get; set; } = [];
    }
}