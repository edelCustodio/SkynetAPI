using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cyber.API.Models.DTO
{
    public class ProductoDTO
    {
        public int idProducto { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public int cantidad { get; set; }
        public decimal precio { get; set; }
        public string imagen { get; set; }
        public string linkFabricante { get; set; }
        public int idCatProducto { get; set; }
        // public CategoriaProducto CategoriaProducto { get; set; }
        public List<TicketDetalleDTO> TicketDetalles { get; set; }
    }
}