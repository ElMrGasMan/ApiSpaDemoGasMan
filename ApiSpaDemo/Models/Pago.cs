using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ApiSpaDemo.Models
{
    public class Pago
    {
        [Key]
        public int PagoId { get; set; }

        public string? UsuarioId { get; set; }
        [ForeignKey("UsuarioId")]
        public Usuario? UsuarioClass { get; set; }

        // ReservaId se setea a "nullable" ya que si Reserva se elimina
        // aun se puede necesitar el pago asociado.
        public int? ReservaId { get; set; }
        [Required]
        [ForeignKey("ReservaId")]
        public Reserva? ReservaClass { get; set; }

        [Required]
        public string FormatoPago { get; set; } = "";
        [Required]
        [DataType(DataType.Currency)]
        public decimal MontoTotal { get; set; }
        [Required]
        public bool Pagado { get; set; } // Indicador de si el pago fue confirmado

    }
}
