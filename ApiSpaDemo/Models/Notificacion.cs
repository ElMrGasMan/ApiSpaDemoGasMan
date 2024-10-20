using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiSpaDemo.Models
{
    public class Notificacion
    {
        [Key]
        public int NotificacionId { get; set; }

        // Este es el usuario al cual irá dirigida la notificacion.
        // Como una reserva de un nuevo turno para un empleado por ejemplo.
        [Required]
        [ForeignKey("UsuarioId")]
        public Usuario UsuarioClass { get; set; }
        [Required]
        public string UsuarioId { get; set; }

        [MaxLength(100, ErrorMessage = "Por temas de espacio solo se permiten 100 caracteres.")]
        public string Descripcion { get; set; } = "";
        public bool Leido { get; set; }
    }
}
