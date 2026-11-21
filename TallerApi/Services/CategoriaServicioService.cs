using Microsoft.EntityFrameworkCore;
using TallerApi.Data;
using TallerApi.Entities;

namespace TallerApi.Services
{
    public class CategoriaServicioService : ICategoriaServicioService
    {
        private readonly TallerDbContext _context;

        public CategoriaServicioService(TallerDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CategoriaServicioEntity>> ObtenerTodasAsync()
        {
            return await _context.CategoriasServicios.ToListAsync();
        }

        public async Task<CategoriaServicioEntity?> ObtenerPorIdAsync(int id)
        {
            return await _context.CategoriasServicios.FindAsync(id);
        }

        public async Task<CategoriaServicioEntity> CrearAsync(CategoriaServicioEntity categoria)
        {
            _context.CategoriasServicios.Add(categoria);
            await _context.SaveChangesAsync();
            return categoria;
        }
    }
}