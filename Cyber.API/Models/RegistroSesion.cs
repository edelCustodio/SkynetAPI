namespace Cyber.API.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Entidad.RegistroSesion")]
    public partial class RegistroSesion
    {
        [Key]
        public int idSesion { get; set; }

        public int idUsuario { get; set; }

        public DateTime fechaInicio { get; set; }

        public DateTime? fechaFin { get; set; }

        public virtual Usuario Usuario { get; set; }
    }
}
