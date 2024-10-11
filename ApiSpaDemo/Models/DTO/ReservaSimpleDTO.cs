using System.ComponentModel.DataAnnotations;

namespace ApiSpaDemo.Models.DTO
{
    public class ReservaSimpleDTO
    {
        [Key]
        public int ReservaId { get; set; }
        [MaxLength(40, ErrorMessage = "El nombre identificativo de la Reserva no puede superar los 40 caracteres.")]
        public string NombreIdentificador { get; set; } = "";
    }
}
