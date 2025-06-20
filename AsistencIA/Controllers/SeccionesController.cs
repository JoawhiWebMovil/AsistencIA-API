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

        // GET: api/Secciones/alumnos
        [HttpGet("alumnos/{idSeccion}")]
        public IActionResult GetUsuariosPorSeccion(int idSeccion)
        {
            var usuarios = (from m in _context.Matriculas
                            join u in _context.Usuarios on m.IdUsuario equals u.IdUsuario
                            where m.IdSeccion == idSeccion && u.Rol == "alumno"
                            select new
                            {
                                idUsuario = u.IdUsuario,
                                nombreCompleto = u.Nombre + " " + u.Apellidos,
                                foto = u.FotoReferencia
                            }).ToList();

            return Ok(usuarios);
        }

        [HttpGet("sesiones/{idSeccion}")]
        public IActionResult GetSesionesPorSeccion(int idSeccion)
        {
            var sesiones = _context.Sesiones
                .Where(s => s.IdSeccion == idSeccion)
                .Select(s => new
                {
                    s.IdSesion,
                    s.Fecha,
                    s.HoraInicio,
                    s.ImagenUrl,
                    s.Estado,
                    s.TipoIngreso
                })
                .ToList();

            return Ok(sesiones);
        }

        private bool SeccionesExists(int id)
        {
            return _context.Secciones.Any(e => e.IdSeccion == id);
        }
    }
}
