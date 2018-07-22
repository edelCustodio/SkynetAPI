namespace Cyber.API.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Entidad.RegistroComputadora")]
    public partial class RegistroComputadora
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public RegistroComputadora()
        {
            RegistroUsoAccesorios = new HashSet<RegistroUsoAccesorio>();
        }

        [Key]
        public int idRegistro { get; set; }

        public int idComputadora { get; set; }

        public DateTime fechaInicio { get; set; }

        public DateTime? fechaFin { get; set; }

        public int? minutos { get; set; }

        [Column(TypeName = "money")]
        public decimal? total { get; set; }

        [Column(TypeName = "money")]
        public decimal? totalPagar { get; set; }

        public bool pagado { get; set; }

        public virtual Computadora Computadora { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RegistroUsoAccesorio> RegistroUsoAccesorios { get; set; }
    }
}
