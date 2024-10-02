using System.ComponentModel.DataAnnotations;

namespace ApiSpaDemo.Models.DTO
{
    public class MensajePrivadoDTO
    {
        [Key]
        public int MensajePrivadoId { get; set; }

        [Required]
        public int ChatId { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "No se puede superar los 100 caracteres.")]
        public string Texto { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime FechaEnvio { get; set; }
    }
}
