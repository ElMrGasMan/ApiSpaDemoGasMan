using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ApiSpaDemo.Models
{
    public class MensajePrivado
    {
        [Key]
        public int MensajePrivadoId { get; set; }

        public int? ChatId { get; set; }
        [ForeignKey("ChatId")]
        public ChatPrivado? ChatPrivado { get; set; }

        [Required]
        public string UsuarioId { get; set; }
        [Required]
        [ForeignKey("UsuarioId")]
        public Usuario Usuario { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "No se puede superar los 100 caracteres.")]
        public string Texto { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime FechaEnvio { get; set; }
    }
}
