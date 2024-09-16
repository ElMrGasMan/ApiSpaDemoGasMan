using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiSpaDemo.Models.DTO
{
    public class PreguntaDTO
    {
        [Key]
        public int PreguntaId { get; set; }
        public string? UsuarioId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateOnly FechaPublicacion { get; set; }
        [Required]
        [MaxLength(100, ErrorMessage="La pregunta no puede superar los 100 caracteres.")]
        public string? Descripcion { get; set; }
    }
}