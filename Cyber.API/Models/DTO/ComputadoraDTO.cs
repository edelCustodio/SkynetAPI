using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cyber.API.Models.DTO
{
    public class ComputadoraDTO
    {
        public int idComputadora { get; set; }
        public string IP { get; set; }
        public string nombre { get; set; }
        public decimal? costoRenta { get; set; }
        public bool enLinea { get; set; }
        public virtual List<RegistroComputadora> RegistroComputadoras { get; set; }
    }
}