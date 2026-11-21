using TallerApi.Models;

namespace TallerApi.Services
{
    public interface IProductoService
    {
        Task<IEnumerable<ProductoDto>> ObtenerProductosAsync();
        Task<ProductoDto?> ObtenerPorIdAsync(int id);
        Task<ProductoDto> CrearAsync(ProductoDto dto);
    }
}
