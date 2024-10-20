using System.ComponentModel.DataAnnotations;

namespace ApiSpaDemo.Models.DTO
{
    public class ReservaDTO
    {
        [Key]
        public int ReservaId { get; set; }

        public string ClienteId { get; set; }

        public ICollection<TurnoDTO> Turnos { get; set; } = [];
        public PagoDTO Pago { get; set; } = new PagoDTO();
        [MaxLength(40, ErrorMessage = "El nombre identificativo de la Reserva no puede superar los 40 caracteres.")]
        public string NombreIdentificador { get; set; } = "";
    }
}
