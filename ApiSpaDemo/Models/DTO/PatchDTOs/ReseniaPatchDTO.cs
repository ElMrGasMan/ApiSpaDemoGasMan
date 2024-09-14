using System.ComponentModel.DataAnnotations;

namespace ApiSpaDemo.Models.DTO.PatchDTOs
{
    public class ReseniaPatchDTO
    {
        [Required]
        [MaxLength(300, ErrorMessage="La reseña no puede contener más de 300 caracteres.")]
        public string? Descripcion { get; set; }
        [Required]
        [Range(1, 5, ErrorMessage = "Valor fuera del rango: (1 a 5).")]
        public short Puntuacion { get; set; }
    }
}