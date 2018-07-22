namespace Cyber.API.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Catalogo.Accesorio")]
    public partial class Accesorio
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Accesorio()
        {
            RegistroUsoAccesorios = new HashSet<RegistroUsoAccesorio>();
        }

        [Key]
        public int idAccesorio { get; set; }

        [Column("accesorio")]
        [Required]
        [StringLength(50)]
        public string accesorio1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RegistroUsoAccesorio> RegistroUsoAccesorios { get; set; }
    }
}
