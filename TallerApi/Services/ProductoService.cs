using Microsoft.EntityFrameworkCore;
using TallerApi.Data;
using TallerApi.Entities;
using TallerApi.Models;

namespace TallerApi.Services
{
    public class ProductoService : IProductoService
    {
        private readonly TallerDbContext _context;

        public ProductoService(TallerDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProductoDto>> ObtenerProductosAsync()
        {
            return await _context.Productos
                .Select(p => new ProductoDto
                {
                    IdProductos = p.IdProductos,
                    IdCategoria = p.IdCategoria,
                    Nombre = p.Nombre,
                    Descripcion = p.Descripcion,
                    Precio = p.Precio
                })
                .ToListAsync();
        }

        public async Task<ProductoDto?> ObtenerPorIdAsync(int id)
        {
            var p = await _context.Productos.FindAsync(id);
            if (p == null) return null;

            return new ProductoDto
            {
                IdProductos = p.IdProductos,
                IdCategoria = p.IdCategoria,
                Nombre = p.Nombre,
                Descripcion = p.Descripcion,
                Precio = p.Precio
            };
        }

        public async Task<ProductoDto> CrearAsync(ProductoDto dto)
        {
            var entidad = new ProductoEntity
            {
                IdCategoria = dto.IdCategoria,
                Nombre = dto.Nombre,
                Descripcion = dto.Descripcion,
                Precio = dto.Precio,
                Estado = 1
            };

            _context.Productos.Add(entidad);
            await _context.SaveChangesAsync();

            dto.IdProductos = entidad.IdProductos;
            return dto;
        }
    }
}
