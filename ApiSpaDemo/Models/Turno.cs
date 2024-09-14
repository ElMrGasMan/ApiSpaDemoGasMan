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
        public Servicio? ServicioClass { get; set; }

        [Required]
        public ICollection<Usuario> Usuarios { get; set; } = [];

        [Required]
        public string? Frecuencia { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateOnly FechaInicio { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateOnly FechaFinal { get; set; }
        [Required]
        [DataType(DataType.Time)]
        public TimeOnly Horario { get; set; }
        [Required]
        [Display(Name="Cantidad maxima de reservas por turno.")]
        public int CantMaxRsrv { get; set; }
        [Required]
        [Display(Name="Cantidad actual de reservas por turno.")]
        public int CantActlRsrv { get; set; }
    }
}