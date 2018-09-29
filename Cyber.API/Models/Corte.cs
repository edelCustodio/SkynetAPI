namespace Cyber.API.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Entidad.Corte")]
    public partial class Corte
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Corte()
        {
        }

        [Key]
        public int idCorte { get; set; }

        public int idUsuario { get; set; }

        [Column(TypeName = "money")]
        public decimal? montoInicial { get; set; }

        [Column(TypeName = "money")]
        public decimal? montoFinal { get; set; }

        [Column(TypeName = "money")]
        public decimal? montoVentas { get; set; }

        [Column(TypeName = "money")]
        public decimal? diferencia { get; set; }

        public DateTime fechaInicio { get; set; }

        public DateTime? fechaFin { get; set; }

        public virtual Usuario Usuario { get; set; }
    }
}
