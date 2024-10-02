using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ApiSpaDemo.Models
{
    public class Pago
    {
        [Key]
        public int PagoId { get; set; }

        public string? UsuarioId { get; set; }
        [Required]
        [ForeignKey("UsuarioId")]
        public Usuario? UsuarioClass { get; set; }

        public string? ReservaId { get; set; }
        [Required]
        [ForeignKey("ReservaId")]
        public Reserva? ReservaClass { get; set; }

        [Required]
        public string FormatoPago { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        public decimal Monto { get; set; }

    }
}
