namespace Cyber.API.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Entidad.TicketDetalle")]
    public partial class TicketDetalle
    {
        [Key]
        public int idTicketDetalle { get; set; }

        public int idTicket { get; set; }

        public int? idProducto { get; set; }

        [Column(TypeName = "money")]
        public decimal? precio { get; set; }

        public int cantidad { get; set; }

        public bool eliminado { get; set; }

        public virtual Producto Producto { get; set; }

        public virtual Ticket Ticket { get; set; }
    }
}
