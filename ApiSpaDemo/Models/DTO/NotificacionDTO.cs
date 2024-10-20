using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ApiSpaDemo.Models.DTO
{
    public class NotificacionDTO
    {
        [Key]
        public int NotificacionId { get; set; }

        public string UsuarioId { get; set; }

        [MaxLength(100, ErrorMessage = "Por temas de espacio solo se permiten 100 caracteres.")]
        public string Descripcion { get; set; } = "";
        public bool Leido { get; set; }
    }
}
