using System.ComponentModel.DataAnnotations;

namespace ApiSpaDemo.Models.DTO
{
    public class ServicioDTO
    {
        [Key]
        public int ServicioId { get; set; }

        public string? UsuarioId { get; set; }

        public ICollection<TurnoDTO> Turnos { get; set; } = [];
        public ICollection<HorarioServicioDTO> Horarios { get; set; } = [];

        [Required]
        [MaxLength(50, ErrorMessage="El nombre del tipo no puede exceder 50 caracteres.")]
        public string? TipoServicio { get; set; }
        [Required]
        [MaxLength(600, ErrorMessage="La descripcion del servicio no puede superar los 600 caracteres.")]
        public string? Descripcion { get; set; }
        public string? RutaImagen { get; set; }
        [Required]
        [MaxLength(30, ErrorMessage="El titulo del servicio no puede superar los 30 caracteres.")]
        public string? Titulo { get; set; }
        public int DuracionMinut { get; set; }
        public decimal Precio { get; set; }
        public short TiempoLimiteHoras { get; set; }
    }
}