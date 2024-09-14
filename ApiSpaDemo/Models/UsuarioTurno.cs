using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiSpaDemo.Models
{
    public class UsuarioTurno
    {
        [Required]
        public int UsuarioId { get; set; }
        [Required]
        [ForeignKey("UsuarioId")]
        public Usuario? UsuarioClass { get; set; }

        [Required]
        public int TurnoId { get; set; }
        [Required]
        [ForeignKey("TurnoId")]
        public Usuario? TurnoClass { get; set; }
    }
}