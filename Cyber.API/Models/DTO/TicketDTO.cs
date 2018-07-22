using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cyber.API.Models.DTO
{
    public class TicketDTO
    {
        public int idTicket { get; set; }
        public decimal total { get; set; }
        public decimal pago { get; set; }
        public decimal cambio { get; set; }
        public int? idRegistro { get; set; }
        public DateTime fecha { get; set; }
        public bool status { get; set; }
        public bool eliminado { get; set; }
        public int? idUsuario { get; set; }
        public List<TicketDetalleDTO> Detalle { get; set; }
    }
}