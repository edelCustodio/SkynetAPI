using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cyber.API.Models.DTO
{
    public class RegistroComputadoraDTO
    {
        public int idRegistro { get; set; }
        public int idComputadora { get; set; }
        public DateTime fechaInicio { get; set; }
        public DateTime? fechaFin { get; set; }
        public int? minutos { get; set; }
        public decimal? total { get; set; }
        public decimal? totalPagar { get; set; }
        public bool pagado { get; set; }
        public virtual Computadora Computadora { get; set; }
        public virtual ICollection<RegistroUsoAccesorio> RegistroUsoAccesorios { get; set; }
    }
}