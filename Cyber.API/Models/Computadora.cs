namespace Cyber.API.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Catalogo.Computadora")]
    public partial class Computadora
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Computadora()
        {
            RegistroComputadoras = new HashSet<RegistroComputadora>();
        }

        [Key]
        public int idComputadora { get; set; }

        [Required]
        [StringLength(20)]
        public string IP { get; set; }

        [StringLength(50)]
        public string nombre { get; set; }

        [Column(TypeName = "money")]
        public decimal? costoRenta { get; set; }

        public bool enLinea { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RegistroComputadora> RegistroComputadoras { get; set; }
    }
}
