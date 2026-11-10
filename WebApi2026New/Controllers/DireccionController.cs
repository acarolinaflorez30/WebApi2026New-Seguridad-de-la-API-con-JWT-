using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi2026New.Data;
using WebApi2026New.DTOs;
using WebApi2026New.Models;

namespace WebApi2026New.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DireccionController : ControllerBase
    {
        private readonly AppDBContext _context;

        public DireccionController(AppDBContext context)
        {
            _context = context;
        }

        // GET: api/Direccion
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DireccionDTO>>> GetDirecciones()
        {
            return await _context.Direcciones
                .Select(d => new DireccionDTO
                {
                    idDireccion = d.idDireccion,
                    calle = d.calle,
                    ciudad = d.ciudad,
                    departamento = d.departamento,
                    codigoPostal = d.codigoPostal,
                    pais = d.pais
                })
                .ToListAsync();
        }

        // GET: api/Direccion/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DireccionDTO>> GetDireccion(int id)
        {
            var direccion = await _context.Direcciones
                .Where(d => d.idDireccion == id)
                .Select(d => new DireccionDTO
                {
                    idDireccion = d.idDireccion,
                    calle = d.calle,
                    ciudad = d.ciudad,
                    departamento = d.departamento,
                    codigoPostal = d.codigoPostal,
                    pais = d.pais
                })
                .FirstOrDefaultAsync();

            if (direccion == null)
            {
                return NotFound();
            }

            return direccion;
        }

        // DELETE: api/Direccion/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDireccion(int id)
        {
            var direccion = await _context.Direcciones.FindAsync(id);
            if (direccion == null)
            {
                return NotFound();
            }

            _context.Direcciones.Remove(direccion);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}