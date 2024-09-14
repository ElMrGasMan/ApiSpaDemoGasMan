using System.ComponentModel.DataAnnotations;

namespace ApiSpaDemo.Models.DTO.PatchDTOs
{
    public class RespuestaPatchDTO
    {
        [Required]
        [MaxLength(150, ErrorMessage ="La respuesta no puede superar los 150 caracteres.")]
        public string? Descripcion { get; set; }
    }
}