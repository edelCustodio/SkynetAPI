namespace Cyber.API.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class CyberModel : DbContext
    {
        public CyberModel()
            : base("name=CyberModel")
        {
        }

        public virtual DbSet<Accesorio> Accesorios { get; set; }
        public virtual DbSet<CategoriaProducto> CategoriaProductos { get; set; }
        public virtual DbSet<Computadora> Computadoras { get; set; }
        public virtual DbSet<Producto> Productos { get; set; }
        public virtual DbSet<TipoUsuario> TipoUsuarios { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<Nota> Notas { get; set; }
        public virtual DbSet<RegistroComputadora> RegistroComputadoras { get; set; }
        public virtual DbSet<RegistroSesion> RegistroSesiones { get; set; }
        public virtual DbSet<RegistroUsoAccesorio> RegistroUsoAccesorios { get; set; }
        public virtual DbSet<Ticket> Tickets { get; set; }
        public virtual DbSet<TicketDetalle> TicketDetalles { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }
        public virtual DbSet<Corte> Cortes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Accesorio>()
                .Property(e => e.accesorio1)
                .IsUnicode(false);

            modelBuilder.Entity<Accesorio>()
                .HasMany(e => e.RegistroUsoAccesorios)
                .WithRequired(e => e.Accesorio)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CategoriaProducto>()
                .Property(e => e.nombre)
                .IsUnicode(false);

            modelBuilder.Entity<CategoriaProducto>()
                .Property(e => e.descripcion)
                .IsUnicode(false);

            modelBuilder.Entity<CategoriaProducto>()
                .HasMany(e => e.Productos)
                .WithRequired(e => e.CategoriaProducto)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Computadora>()
                .Property(e => e.IP)
                .IsUnicode(false);

            modelBuilder.Entity<Computadora>()
                .Property(e => e.nombre)
                .IsUnicode(false);

            modelBuilder.Entity<Computadora>()
                .Property(e => e.costoRenta)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Computadora>()
                .HasMany(e => e.RegistroComputadoras)
                .WithRequired(e => e.Computadora)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Producto>()
                .Property(e => e.nombre)
                .IsUnicode(false);

            modelBuilder.Entity<Producto>()
                .Property(e => e.descripcion)
                .IsUnicode(false);

            modelBuilder.Entity<Producto>()
                .Property(e => e.precio)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Producto>()
                .Property(e => e.imagen)
                .IsUnicode(false);

            modelBuilder.Entity<Producto>()
                .Property(e => e.linkFabricante)
                .IsUnicode(false);

            modelBuilder.Entity<TipoUsuario>()
                .Property(e => e.tipoUsuario1)
                .IsUnicode(false);

            modelBuilder.Entity<TipoUsuario>()
                .HasMany(e => e.Usuarios)
                .WithRequired(e => e.TipoUsuario)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Nota>()
                .Property(e => e.nota1)
                .IsUnicode(false);

            modelBuilder.Entity<RegistroComputadora>()
                .Property(e => e.total)
                .HasPrecision(19, 4);

            modelBuilder.Entity<RegistroComputadora>()
                .Property(e => e.totalPagar)
                .HasPrecision(19, 4);

            modelBuilder.Entity<RegistroComputadora>()
                .HasMany(e => e.RegistroUsoAccesorios)
                .WithRequired(e => e.RegistroComputadora)
                .HasForeignKey(e => e.idRegistroComputadora)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Ticket>()
                .Property(e => e.total)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Ticket>()
                .Property(e => e.pago)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Ticket>()
                .Property(e => e.cambio)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Ticket>()
               .HasMany(e => e.Detalle)
               .WithRequired(e => e.Ticket)
               .WillCascadeOnDelete(false);

            modelBuilder.Entity<TicketDetalle>()
                .Property(e => e.precio)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Usuario>()
                .Property(e => e.nombreCompleto)
                .IsUnicode(false);

            modelBuilder.Entity<Usuario>()
                .Property(e => e.correoElectronico)
                .IsUnicode(false);

            modelBuilder.Entity<Usuario>()
                .Property(e => e.usuario1)
                .IsUnicode(false);

            modelBuilder.Entity<Usuario>()
                .Property(e => e.contraseña)
                .IsUnicode(false);

            modelBuilder.Entity<Usuario>()
                .HasMany(e => e.RegistroSesions)
                .WithRequired(e => e.Usuario)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Usuario>()
                .HasMany(e => e.Cortes)
                .WithRequired(e => e.Usuario)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Corte>()
                .Property(e => e.montoInicial)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Corte>()
                .Property(e => e.montoFinal)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Corte>()
                .Property(e => e.montoVentas)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Corte>()
                .Property(e => e.diferencia)
                .HasPrecision(19, 4);
        }
    }
}
