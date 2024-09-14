using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiSpaDemo.Models
{
    public class Resenia
    {
        [Key]
        public int ReseniaId { get; set; }

        [Required]
        public string? UsuarioId { get; set; }
        [Required]
        [ForeignKey("UsuarioId")]
        public Usuario? UsuarioClass { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime FechaPublicacion { get; set; }
        [Required]
        [MaxLength(300, ErrorMessage="La reseña no puede contener más de 300 caracteres.")]
        public string? Descripcion { get; set; }
        [Required]
        [Range(1, 5, ErrorMessage = "Valor fuera del rango: (1 a 5).")]
        public short Puntuacion { get; set; }
    }
}