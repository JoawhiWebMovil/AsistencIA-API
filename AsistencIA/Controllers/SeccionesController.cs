using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AsistencIA_DOMAIN.Data;

namespace AsistencIA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeccionesController : ControllerBase
    {
        private readonly DbAsistencIaDbContext _context;

        public SeccionesController(DbAsistencIaDbContext context)
        {
            _context = context;
        }

        // GET: api/Secciones
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Secciones>>> GetSecciones()
        {
            return await _context.Secciones.ToListAsync();
        }

        // GET: api/Secciones/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Secciones>> GetSecciones(int id)
        {
            var secciones = await _context.Secciones.FindAsync(id);

            if (secciones == null)
            {
                return NotFound();
            }

            return secciones;
        }

        // PUT: api/Secciones/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSecciones(int id, Secciones secciones)
        {
            if (id != secciones.IdSeccion)
            {
                return BadRequest();
            }

            _context.Entry(secciones).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SeccionesExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Secciones
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Secciones>> PostSecciones(Secciones secciones)
        {
            _context.Secciones.Add(secciones);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSecciones", new { id = secciones.IdSeccion }, secciones);
        }

        // DELETE: api/Secciones/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSecciones(int id)
        {
            var secciones = await _context.Secciones.FindAsync(id);
            if (secciones == null)
            {
                return NotFound();
            }

            _context.Secciones.Remove(secciones);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SeccionesExists(int id)
        {
            return _context.Secciones.Any(e => e.IdSeccion == id);
        }
    }
}
