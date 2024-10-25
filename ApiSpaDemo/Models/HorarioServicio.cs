using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ApiSpaDemo.Models
{
    public class HorarioServicio
    {
        [Key]
        public int HorarioServicioId { get; set; }

        public int? ServicioId { get; set; }
        [ForeignKey("ServicioId")]
        public Servicio? ServicioClass { get; set; }

        [DataType(DataType.Time)]
        public TimeOnly HoraInicio { get; set; }
        [DataType(DataType.Time)]
        public TimeOnly HoraFinal { get; set; }
    }
}
