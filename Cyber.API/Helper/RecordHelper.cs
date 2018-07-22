using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cyber.API.Helper
{
    public class RecordHelper
    {
    }

    public class RecordParameters
    {
        public int idComputadora { get; set; }
        public DateTime fecha { get { return DateTime.Now; } }
        public int minutos { get; set; }
        public int idUsuario { get; set; }
    }
}