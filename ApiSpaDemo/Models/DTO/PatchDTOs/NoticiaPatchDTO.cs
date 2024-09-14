using System.ComponentModel.DataAnnotations;

namespace ApiSpaDemo.Models.DTO.PatchDTOs
{
    public class NoticiaPatchDTO
    {
        [Required]
        [MaxLength(50, ErrorMessage ="El titulo de la noticia no puede exceder 50 caracteres.")]
        public string? Titulo { get; set; }
        public string? RutaImagen { get; set; }
        [Required]
        public string? RutaPDF { get; set; }
    }
}