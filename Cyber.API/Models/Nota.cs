namespace Cyber.API.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Entidad.Nota")]
    public partial class Nota
    {
        [Key]
        public int idNota { get; set; }

        [Column("nota")]
        public string nota1 { get; set; }

        public int idComputadora { get; set; }
    }
}
