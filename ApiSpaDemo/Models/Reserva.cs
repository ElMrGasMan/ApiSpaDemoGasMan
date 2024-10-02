using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiSpaDemo.Models
{
    public class Reserva
    {
        [Key]
        public int ReservaId { get; set; }

        // Clave foránea del Usuario (Cliente)
        public string ClienteId { get; set; }
        [Required]
        [ForeignKey("ClienteId")]
        public Usuario Cliente { get; set; } 

        // Clave foránea del Usuario (Empleado que atiende el servicio)
        public string EmpleadoId { get; set; }
        [Required]
        [ForeignKey("EmpleadoId")]
        public Usuario Empleado { get; set; } 

        public int ServicioId { get; set; }
        [Required]
        [ForeignKey("ServicioId")]
        public Servicio Servicio { get; set; }

        public DateTime HoraInicio { get; set; }
        public int DuracionMinut { get; set; }
        [MaxLength(150, ErrorMessage = "No se puede superar los 200 caracteres.")]
        public string Consideraciones { get; set; }
    }
}
