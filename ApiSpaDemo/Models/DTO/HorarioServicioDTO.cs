using System.ComponentModel.DataAnnotations;

namespace ApiSpaDemo.Models.DTO
{
    public class HorarioServicioDTO
    {
        [Key]
        public int HorarioServicioId { get; set; }

        public int? ServicioId { get; set; }

        [DataType(DataType.Time)]
        public TimeOnly FechaInicio { get; set; }
        [DataType(DataType.Time)]
        public TimeOnly FechaFinal { get; set; }
    }
}
