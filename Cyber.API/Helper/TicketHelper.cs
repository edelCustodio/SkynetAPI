using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cyber.API.Helper
{
    public static class TicketHelper
    {
    }

    public class TicketDetalleInsert
    {
        public string strInsert { get; set; }
    }

    public class TicketDetalleUpdate
    {
        public int idTicketDetalle { get; set; }
        public decimal cantidad { get; set; }
        public decimal precio { get; set; }
    }
}