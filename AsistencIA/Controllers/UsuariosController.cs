using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AsistencIA_DOMAIN.Data;
using PromcoserDOMAIN.Core.DTOs;
using AsistencIA_DOMAIN.Core.Interfaces;

namespace AsistencIA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly DbAsistencIaDbContext _context;
        private readonly IUsuariosService _usuariosService;

        public UsuariosController(IUsuariosService usuariosService, DbAsistencIaDbContext context)
        {
            _usuariosService = usuariosService;
            _context = context;
        }


        [HttpPost("LogIn")]
        public async Task<IActionResult> SignIn([FromBody] UsuariosAuthDTO userAuthDTO)
        {
            if (userAuthDTO.Usuario == "" || userAuthDTO.Contrasena == "") return BadRequest();

            var result = await _usuariosService.SignIn(userAuthDTO.Usuario, userAuthDTO.Contrasena);
            if (result == null) return NotFound();
            return Ok(result);
        }

        // GET: api/Usuarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuarios>>> GetUsuarios()
        {
            return await _context.Usuarios.ToListAsync();
        }

        // GET: api/Usuarios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuarios>> GetUsuarios(int id)
        {
            var usuarios = await _context.Usuarios.FindAsync(id);

            if (usuarios == null)
            {
                return NotFound();
            }

            return usuarios;
        }

        // PUT: api/Usuarios/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuarios(string id, Usuarios usuarios)
        {
            if (id != usuarios.IdUsuario)
            {
                return BadRequest();
            }

            _context.Entry(usuarios).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuariosExists(id))
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

        // POST: api/Usuarios
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Usuarios>> PostUsuarios(Usuarios usuarios)
        {
            _context.Usuarios.Add(usuarios);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUsuarios", new { id = usuarios.IdUsuario }, usuarios);
        }

        // DELETE: api/Usuarios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuarios(int id)
        {
            var usuarios = await _context.Usuarios.FindAsync(id);
            if (usuarios == null)
            {
                return NotFound();
            }

            _context.Usuarios.Remove(usuarios);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UsuariosExists(string id)
        {
            return _context.Usuarios.Any(e => e.IdUsuario == id);
        }
    }
}
