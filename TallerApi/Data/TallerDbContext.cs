using Microsoft.EntityFrameworkCore;
using TallerApi.Entities;

namespace TallerApi.Data
{
    public class TallerDbContext : DbContext
    {
        public TallerDbContext(DbContextOptions<TallerDbContext> options) : base(options)
        {
        }

        public DbSet<ProductoEntity> Productos { get; set; }
        public DbSet<CategoriaServicioEntity> CategoriasServicios { get; set; }
        public DbSet<ClienteCarroEntity> ClientesCarros { get; set; }
        public DbSet<MarcaCarroEntity> MarcasCarros { get; set; }
        public DbSet<CarroEntity> Carros { get; set; }
        public DbSet<CotizacionEntity> Cotizaciones { get; set; }
        public DbSet<DetalleCotizacionEntity> DetallesCotizaciones { get; set; }
        public DbSet<FacturaEntity> Facturas { get; set; }
        public DbSet<DetalleFacturaEntity> DetallesFacturas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            // Configuraciones adicionales si fuera necesario (mapeo exacto de tablas)
            modelBuilder.Entity<ProductoEntity>().ToTable("producto");
            modelBuilder.Entity<CategoriaServicioEntity>().ToTable("categoriaServicios");
            modelBuilder.Entity<ClienteCarroEntity>().ToTable("clienteCarro");
            modelBuilder.Entity<MarcaCarroEntity>().ToTable("marcaCarro");
            modelBuilder.Entity<CarroEntity>().ToTable("carro");
            modelBuilder.Entity<CotizacionEntity>().ToTable("cotizacion");
            modelBuilder.Entity<DetalleCotizacionEntity>().ToTable("detalleCotizacion");
            modelBuilder.Entity<FacturaEntity>().ToTable("factura");
            modelBuilder.Entity<DetalleFacturaEntity>().ToTable("detalleFactura");
        }
    }
}
