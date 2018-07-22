namespace Cyber.API.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Entidad.RegistroUsoAccesorio")]
    public partial class RegistroUsoAccesorio
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int idRegistroAccesorio { get; set; }

        public int idAccesorio { get; set; }

        public int idRegistroComputadora { get; set; }

        public virtual Accesorio Accesorio { get; set; }

        public virtual RegistroComputadora RegistroComputadora { get; set; }
    }
}
