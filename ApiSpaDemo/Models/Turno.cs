using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiSpaDemo.Models
{
    public class Turno
    {
        [Key]
        public int TurnoId { get; set; }

        [Required]
        public int ServicioId { get; set; }
        [Required]
        [ForeignKey("ServicioId")]
        public Servicio ServicioClass { get; set; } 

        public int? ReservaId { get; set; }
        [ForeignKey("ReservaId")]
        public Reserva? ReservaClass { get; set; } 

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime FechaInicio { get; set; }
        [MaxLength(150, ErrorMessage = "Por favor, use menos 150 caracteres.")]
        public string? Descripcion { get; set; }
    }
}
