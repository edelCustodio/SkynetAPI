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

        public decimal precioFinal
        {
            get
            {
                decimal value = 0;
                if (this.precio != null)
                {
                    value = this.precio.Value;
                }

                if (this.Producto != null && this.Producto.idProducto != 1360 && this.Producto.precio > 0)
                {
                    value = this.Producto.precio;
                }

                if (this.Registro != null && this.Producto.idProducto == 1360)
                {
                    value = this.Registro.totalPagar.Value;
                }

                return value;
            }
        }

        public decimal total
        {
            get
            {
                return this.precioFinal * this.cantidad;
            }
        }

        public string precioFinalStr
        {
            get
            {
                return string.Format("{0:C}", this.precioFinal);
            }
        }

        public string totalStr
        {
            get
            {
                return string.Format("{0:C}", this.total);
            }
        }

        public RegistroComputadoraDTO Registro { get; set; }
        public ProductoDTO Producto { get; set; }
        public TicketDTO Ticket { get; set; }
    }
}