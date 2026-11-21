using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TallerApi.Data;
using TallerApi.Entities; // Importante para reconocer CategoriaServicioEntity

namespace TallerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasServiciosController : ControllerBase
    {
        private readonly TallerDbContext _context;

        public CategoriasServiciosController(TallerDbContext context)
        {
            _context = context;
        }

        // GET: api/CategoriasServicios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoriaServicioEntity>>> GetCategorias()
        {
            return await _context.CategoriasServicios.ToListAsync();
        }

        // POST: api/CategoriasServicios
        [HttpPost]
        public async Task<ActionResult<CategoriaServicioEntity>> PostCategoria(CategoriaServicioEntity categoria)
        {
            _context.CategoriasServicios.Add(categoria);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCategorias), new { id = categoria.IdCategoria }, categoria);
        }
    }
}