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
    public class SesionesController : ControllerBase
    {
        private readonly DbAsistencIaDbContext _context;

        public SesionesController(DbAsistencIaDbContext context)
        {
            _context = context;
        }

        // GET: api/Sesiones
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sesiones>>> GetSesiones()
        {
            return await _context.Sesiones.ToListAsync();
        }

        // GET: api/Sesiones/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Sesiones>> GetSesiones(int id)
        {
            var sesiones = await _context.Sesiones.FindAsync(id);

            if (sesiones == null)
            {
                return NotFound();
            }

            return sesiones;
        }

        // PUT: api/Sesiones/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSesiones(int id, Sesiones sesiones)
        {
            if (id != sesiones.IdSesion)
            {
                return BadRequest();
            }

            _context.Entry(sesiones).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SesionesExists(id))
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

        // POST: api/Sesiones
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Sesiones>> PostSesiones(Sesiones sesiones)
        {
            _context.Sesiones.Add(sesiones);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSesiones", new { id = sesiones.IdSesion }, sesiones);
        }

        // DELETE: api/Sesiones/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSesiones(int id)
        {
            var sesiones = await _context.Sesiones.FindAsync(id);
            if (sesiones == null)
            {
                return NotFound();
            }

            _context.Sesiones.Remove(sesiones);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SesionesExists(int id)
        {
            return _context.Sesiones.Any(e => e.IdSesion == id);
        }
    }
}
