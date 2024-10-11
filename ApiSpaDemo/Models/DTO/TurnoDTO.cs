using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ApiSpaDemo.Models.DTO
{
    public class TurnoDTO
    {
        [Key]
        public int IdTurno { get; set; }

        [Required]
        public int ServicioId { get; set; }

        public int? ReservaId { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime FechaInicio { get; set; }
        [MaxLength(150, ErrorMessage = "Por favor, use menos de 150 caracteres.")]
        public string? Descripcion { get; set; }
    }
}
