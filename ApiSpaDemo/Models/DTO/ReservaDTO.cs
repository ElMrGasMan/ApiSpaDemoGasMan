using System.ComponentModel.DataAnnotations;

namespace ApiSpaDemo.Models.DTO
{
    public class ReservaDTO
    {
        [Key]
        public int ReservaId { get; set; }

        // Clave foránea del Usuario (Empleado que atiende el servicio)
        // No es necesario poner la del cliente, pues se obtiene de la autenticacion
        public string EmpleadoId { get; set; }
        public int ServicioId { get; set; }

        public DateTime HoraInicio { get; set; }
        public int DuracionMinut { get; set; }
        [MaxLength(150, ErrorMessage = "No se puede superar los 200 caracteres.")]
        public string Consideraciones { get; set; }
    }
}
