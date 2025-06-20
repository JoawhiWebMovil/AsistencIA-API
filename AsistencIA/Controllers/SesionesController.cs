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

        [HttpGet("asistenciaAlumnos/{idSesion}")]
        public IActionResult GetAlumnosPorSesion(int idSesion)
        {
            // Obtener la sección de la sesión
            var idSeccion = _context.Sesiones
                .Where(s => s.IdSesion == idSesion)
                .Select(s => s.IdSeccion)
                .FirstOrDefault();

            if (idSeccion == 0)
                return NotFound("Sesión no encontrada.");

            var alumnos = (from m in _context.Matriculas
                           join u in _context.Usuarios on m.IdUsuario equals u.IdUsuario
                           join a in _context.Asistencias
                               on new { m.IdUsuario, IdSesion = idSesion } equals new { a.IdUsuario, a.IdSesion }
                               into asistenciaJoin
                           from asistencia in asistenciaJoin.DefaultIfEmpty()
                           where m.IdSeccion == idSeccion && u.Rol == "alumno"
                           select new
                           {
                               idUsuario = u.IdUsuario,
                               nombreCompleto = u.Nombre + " " + u.Apellidos,
                               foto = u.FotoReferencia,
                               estado = asistencia != null ? asistencia.Estado : null,                     
                           }).ToList();

            return Ok(alumnos);
        }

        private bool SesionesExists(int id)
        {
            return _context.Sesiones.Any(e => e.IdSesion == id);
        }
    }
}
