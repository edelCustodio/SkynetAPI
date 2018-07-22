namespace Cyber.API.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Entidad.Usuario")]
    public partial class Usuario
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Usuario()
        {
            RegistroSesions = new HashSet<RegistroSesion>();
        }

        [Key]
        public int idUsuario { get; set; }

        [Required]
        [StringLength(150)]
        public string nombreCompleto { get; set; }

        [Required]
        [StringLength(50)]
        public string correoElectronico { get; set; }

        [Column("usuario")]
        [Required]
        [StringLength(50)]
        public string usuario1 { get; set; }

        [Required]
        public string contraseña { get; set; }

        public int idTipoUsuario { get; set; }

        public virtual TipoUsuario TipoUsuario { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RegistroSesion> RegistroSesions { get; set; }
    }
}
