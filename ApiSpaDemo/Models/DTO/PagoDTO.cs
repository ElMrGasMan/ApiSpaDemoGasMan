using System.ComponentModel.DataAnnotations;

namespace ApiSpaDemo.Models.DTO
{
    public class PagoDTO
    {
        [Key]
        public int PagoId { get; set; }

        public string? ReservaId { get; set; }

        [Required]
        public string FormatoPago { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        public decimal Monto { get; set; }
    }
}
