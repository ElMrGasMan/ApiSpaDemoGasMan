using System.ComponentModel.DataAnnotations;

namespace ApiSpaDemo.Models.DTO
{
    public class HorarioServicioDTO
    {
        [Key]
        public int HorarioServicioId { get; set; }

        public int? ServicioId { get; set; }

        [DataType(DataType.Time)]
        public TimeSpan HoraInicio { get; set; }
        [DataType(DataType.Time)]
        public TimeSpan HoraFinal { get; set; }
    }
}
