﻿using System.ComponentModel.DataAnnotations;

namespace ApiSpaDemo.Models.DTO
{
    public class ReservaDTO
    {
        [Key]
        public int ReservaId { get; set; }

        // Clave foránea del Usuario (Empleado que atiende el servicio)
        // No es necesario poner la del cliente, pues se obtiene de la autenticacion
        public ICollection<TurnoDTO> Turnos { get; set; } = [];
        public PagoDTO Pago { get; set; } = new PagoDTO();
        [MaxLength(40, ErrorMessage = "El nombre identificativo de la Reserva no puede superar los 40 caracteres.")]
        public string NombreIdentificador { get; set; } = "";
    }
}
