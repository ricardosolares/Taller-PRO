using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TallerApi.Data;
using TallerApi.Entities; // <-- Importante para reconocer ProductoEntity

namespace TallerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly TallerDbContext _context;

        public ProductosController(TallerDbContext context)
        {
            _context = context;
        }

        // GET: api/Productos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductoEntity>>> GetProductos()
        {
            return await _context.Productos.ToListAsync();
        }

        // POST: api/Productos
        [HttpPost]
        public async Task<ActionResult<ProductoEntity>> PostProducto(ProductoEntity producto)
        {
            _context.Productos.Add(producto);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProductos), new { id = producto.IdProductos }, producto);
        }
    }
}