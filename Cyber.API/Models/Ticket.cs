namespace Cyber.API.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Entidad.Ticket")]
    public partial class Ticket
    {
        [Key]
        public int idTicket { get; set; }

        [Column(TypeName = "money")]
        public decimal total { get; set; }

        [Column(TypeName = "money")]
        public decimal pago { get; set; }

        [Column(TypeName = "money")]
        public decimal cambio { get; set; }

        public int? idRegistro { get; set; }

        public DateTime fecha { get; set; }

        public bool status { get; set; }

        public bool eliminado { get; set; }

        public int? idUsuario { get; set; }

        public virtual ICollection<TicketDetalle> Detalle { get; set; }
    }
}
