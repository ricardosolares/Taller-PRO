using TallerApi.Entities;

namespace TallerApi.Services
{
    public interface ICategoriaServicioService
    {
        Task<IEnumerable<CategoriaServicioEntity>> ObtenerTodasAsync();
        Task<CategoriaServicioEntity?> ObtenerPorIdAsync(int id);
        Task<CategoriaServicioEntity> CrearAsync(CategoriaServicioEntity categoria);
    }
}