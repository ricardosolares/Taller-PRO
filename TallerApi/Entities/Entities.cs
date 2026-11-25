using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TallerApi.Entities
{
    public class CategoriaServicioEntity
    {
        [Key]
        [Column("id_categoria")]
        public int IdCategoria { get; set; }
        [Column("nombre")]
        public required string Nombre { get; set; }
        [Column("descripcion")]
        public string? Descripcion { get; set; }
        [Column("estado")]
        public byte Estado { get; set; } = 1;
    }

    public class ProductoEntity
    {
        [Key]
        [Column("id_productos")]
        public int IdProductos { get; set; }
        [Column("id_categoria")]
        public int IdCategoria { get; set; }
        [Column("nombre")]
        public required string Nombre { get; set; }
        [Column("descripcion")]
        public string? Descripcion { get; set; }
        [Column("precio")]
        public decimal Precio { get; set; }
        [Column("estado")]
        public byte Estado { get; set; } = 1;
    }

    public class MarcaCarroEntity
    {
        [Key]
        [Column("id_marca")]
        public int IdMarca { get; set; }
        [Column("nombre")]
        public required string Nombre { get; set; }
        [Column("estado")]
        public byte Estado { get; set; } = 1;
    }

    public class ClienteCarroEntity
    {
        [Key]
        [Column("id_cliente")]
        public int IdCliente { get; set; }
        [Column("nombre")]
        public required string Nombre { get; set; }
        [Column("telefono")]
        public string? Telefono { get; set; }
        [Column("direccion")]
        public string? Direccion { get; set; }
        [Column("estado")]
        public byte Estado { get; set; } = 1;
    }

    public class CarroEntity
    {
        [Key]
        [Column("id_carro")]
        public int IdCarro { get; set; }
        [Column("id_cliente")]
        public int IdCliente { get; set; }
        [Column("id_marca")]
        public int IdMarca { get; set; }
        [Column("modelo")]
        public required string Modelo { get; set; }
        [Column("anio")]
        public int Anio { get; set; }
        [Column("placa")]
        public required string Placa { get; set; }
        [Column("estado")]
        public byte Estado { get; set; } = 1;
    }

    public class CotizacionEntity
    {
        [Key]
        [Column("id_cotizacion")]
        public int IdCotizacion { get; set; }
        [Column("id_cliente")]
        public int IdCliente { get; set; }
        [Column("id_carro")]
        public int IdCarro { get; set; }
        [Column("fecha_emision")]
        public DateTime FechaEmision { get; set; } = DateTime.Now;
        [Column("total")]
        public decimal Total { get; set; }
        [Column("estado")]
        public byte Estado { get; set; } = 1;
    }

    public class DetalleCotizacionEntity
    {
        [Key]
        [Column("id_detalle_cotizacion")]
        public int IdDetalleCotizacion { get; set; }
        [Column("id_cotizacion")]
        public int IdCotizacion { get; set; }
        [Column("id_producto")]
        public int IdProducto { get; set; }
        [Column("cantidad")]
        public int Cantidad { get; set; }
        [Column("precio_unitario")]
        public decimal PrecioUnitario { get; set; }
        [Column("subtotal")]
        public decimal Subtotal { get; set; }
    }

    public class FacturaEntity
    {
        [Key]
        [Column("id_factura")]
        public int IdFactura { get; set; }
        [Column("id_cliente")]
        public int IdCliente { get; set; }
        [Column("id_carro")]
        public int IdCarro { get; set; }
        [Column("id_cotizacion")]
        public int? IdCotizacion { get; set; }
        [Column("fecha_emision")]
        public DateTime FechaEmision { get; set; } = DateTime.Now;
        [Column("total")]
        public decimal Total { get; set; }
        [Column("estado")]
        public byte Estado { get; set; } = 1;
    }

    public class DetalleFacturaEntity
    {
        [Key]
        [Column("id_detalle_factura")]
        public int IdDetalleFactura { get; set; }
        [Column("id_factura")]
        public int IdFactura { get; set; }
        [Column("id_producto")]
        public int IdProducto { get; set; }
        [Column("cantidad")]
        public int Cantidad { get; set; }
        [Column("precio_unitario")]
        public decimal PrecioUnitario { get; set; }
        [Column("subtotal")]
        public decimal Subtotal { get; set; }
    }

    public class UsuarioEntities
    {
       
            [Key]
            [Column("IdUsuario")]
            public int IdUsuario { get; set; }

            [Column("NombreCompleto")]
            public string NombreCompleto { get; set; } = string.Empty;

            [Column("NombreUsuario")]
            public required string NombreUsuario { get; set; }

            [Column("PasswordHash")]
            public required string PasswordHash { get; set; }

            [Column("Rol")]
            public string Rol { get; set; } = "Admin";

            [Column("Activo")]
            public bool Activo { get; set; } = true;

            [Column("FechaCreacion")]
            public DateTime FechaCreacion { get; set; } = DateTime.Now;
        }
    }
