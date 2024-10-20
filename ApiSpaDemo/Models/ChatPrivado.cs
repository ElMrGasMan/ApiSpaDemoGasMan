using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ApiSpaDemo.Models
{
    public class ChatPrivado
    {
        [Key]
        public int ChatId { get; set; }
        
        public int ServicioId { get; set; }
        [Required]
        [ForeignKey("ServicioId")]
        public Servicio Servicio { get; set; }
        [Required]
        public string UsuarioId { get; set; }
        [Required]
        [ForeignKey("UsuarioId")]
        public Usuario UsuarioClass { get; set; }

        public ICollection<MensajePrivado> Mensajes { get; set; } = [];
    }
}
