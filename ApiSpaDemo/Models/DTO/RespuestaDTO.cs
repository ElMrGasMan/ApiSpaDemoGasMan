using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiSpaDemo.Models.DTO
{
    public class RespuestaDTO
    {
        [Key]
        public int RespuestaId { get; set; }

        [Required]
        public string? UsuarioId { get; set; }

        [Required]
        public int PreguntaId { get; set; }

        [Required]
        [MaxLength(150, ErrorMessage ="La respuesta no puede superar los 150 caracteres.")]
        public string? Descripcion { get; set; }
    }
}