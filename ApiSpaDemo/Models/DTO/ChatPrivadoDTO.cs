using System.ComponentModel.DataAnnotations;

namespace ApiSpaDemo.Models.DTO
{
    public class ChatPrivadoDTO
    {
        [Key]
        public int ChatId { get; set; }
        public int ServicioId { get; set; }
    }
}
