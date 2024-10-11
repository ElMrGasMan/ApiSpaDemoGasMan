using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;

namespace ApiSpaDemo.Models
{
    public class Reserva
    {
        [Key]
        public int ReservaId { get; set; }

        // Clave foránea del Usuario (Cliente)
        [Required]
        public string ClienteId { get; set; }
        [Required]
        [ForeignKey("ClienteId")]
        public Usuario Cliente { get; set; } 

        public ICollection<Turno> Turnos { get; set; } = [];

        // Relacion 1:1 con Pago
        public Pago? Pago { get; set; }
        [Required]
        [MaxLength(40, ErrorMessage = "El nombre identificativo de la Reserva no puede superar los 40 caracteres.")]
        public string NombreIdentificador { get; set; } = "";
    }
}
