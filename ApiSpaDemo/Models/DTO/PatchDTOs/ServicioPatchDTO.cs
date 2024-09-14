using System.ComponentModel.DataAnnotations;

namespace ApiSpaDemo.Models.DTO.PatchDTOs
{
    public class ServicioPatchDTO
    {
        [Required]
        [MaxLength(600, ErrorMessage="La descripcion del servicio no puede superar los 600 caracteres.")]
        public string? Descripcion { get; set; }
        public string? RutaImagen { get; set; }
        [Required]
        [MaxLength(30, ErrorMessage="El titulo del servicio no puede superar los 30 caracteres.")]
        public string? Titulo { get; set; }
    }
}