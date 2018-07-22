using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cyber.API.Models.DTO
{
    public class TicketDetalleDTO
    {
        public int idTicketDetalle { get; set; }
        public int idTicket { get; set; }
        public int? idProducto { get; set; }
        public decimal? precio { get; set; }
        public int cantidad { get; set; }
        public bool eliminado { get; set; }
        public virtual ProductoDTO Producto { get; set; }
        public TicketDTO Ticket { get; set; }
    }
}