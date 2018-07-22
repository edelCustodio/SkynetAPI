namespace Cyber.API.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Catalogo.Producto")]
    public partial class Producto
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Producto()
        {
            TicketDetalles = new HashSet<TicketDetalle>();
        }

        [Key]
        public int idProducto { get; set; }

        [Required]
        [StringLength(100)]
        public string nombre { get; set; }

        public string descripcion { get; set; }

        public int cantidad { get; set; }

        [Column(TypeName = "money")]
        public decimal precio { get; set; }

        [StringLength(250)]
        public string imagen { get; set; }

        [StringLength(250)]
        public string linkFabricante { get; set; }

        public int idCatProducto { get; set; }

        public virtual CategoriaProducto CategoriaProducto { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TicketDetalle> TicketDetalles { get; set; }
    }
}
