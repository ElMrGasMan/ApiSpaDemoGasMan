using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiSpaDemo.Models
{
    public class Servicio
    {
        [Key]
        public int ServicioId { get; set; }

        [Required]
        public string? UsuarioId { get; set; }
        [Required]
        [ForeignKey("UsuarioId")]
        public Usuario? UsuarioClass { get; set; }

        // Relaci�n con ChatPrivado
        public ICollection<ChatPrivado> ChatsPrivados { get; set; } = [];

        // Relaci�n con Turnos
        public ICollection<Turno> Turnos { get; set; } = [];

        // Relacion con Horarios
        public ICollection<HorarioServicio> Horarios { get; set; } = [];

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

        [DataType(DataType.Currency)]
        public decimal Precio { get; set; }
        public short TiempoLimiteHoras { get; set; }
    }
}