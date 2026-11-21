namespace TallerApi.Models
{
    public class ProductoDto
    {
        public int IdProductos { get; set; }
        public int IdCategoria { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public decimal Precio { get; set; }
    }
}
