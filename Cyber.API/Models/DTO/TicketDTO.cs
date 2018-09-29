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

        public string estado
        {
            get
            {
                string estado = "Pendiente";
                if (this.status)
                {
                    estado = "Pagado";
                }

                if (this.eliminado)
                {
                    estado = "Eliminado";
                }

                return estado;
            }
        }

        public string fechaStr
        {
            get
            {
                return this.fecha.ToString("dd/MM/yyyy h:mm tt");
            }
        }

        public string totalStr
        {
            get
            {
                return string.Format("{0:C}", this.total);
            }
        }

        public string pagoStr
        {
            get
            {
                return string.Format("{0:C}", this.pago);
            }
        }

        public string cambioStr
        {
            get
            {
                return string.Format("{0:C}", this.cambio);
            }
        }


        public List<TicketDetalleDTO> Detalle { get; set; }
        public UsuarioDTO Usuario { get; set; }
    }
}