using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AsistencIA_DOMAIN.Data;
using AsistencIA_DOMAIN.Core.Entities;
using AsistencIA_DOMAIN.Core.DTOs;

namespace AsistencIA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AsistenciasController : ControllerBase
    {
        private readonly DbAsistencIaDbContext _context;

        public AsistenciasController(DbAsistencIaDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult RegistrarAsistencia([FromBody] AsistenciaCreateRequest request)
        {
            // Verificamos que no exista una asistencia duplicada
            var existente = _context.Asistencias
                .FirstOrDefault(a => a.IdSesion == request.IdSesion && a.IdUsuario == request.IdUsuario);

            if (existente != null)
                return Conflict("La asistencia ya ha sido registrada para este usuario en esta sesión.");

            var nueva = new Asistencias
            {
                IdSesion = request.IdSesion,
                IdUsuario = request.IdUsuario,
                Estado = request.Estado,
                Timestamp = DateTime.Now
            };

            _context.Asistencias.Add(nueva);
            _context.SaveChanges();

            return Ok("Asistencia registrada correctamente.");
        }

        [HttpPut]
        public IActionResult ActualizarAsistencia([FromBody] AsistenciaUpdateRequest request)
        {
            var asistencia = _context.Asistencias
                .FirstOrDefault(a => a.IdSesion == request.IdSesion && a.IdUsuario == request.IdUsuario);

            if (asistencia == null)
                return NotFound("Asistencia no encontrada.");

            asistencia.Estado = request.NuevoEstado;
            asistencia.Timestamp = DateTime.Now;

            _context.SaveChanges();

            return Ok("Asistencia actualizada correctamente.");
        }

        

        private bool AsistenciasExists(int id)
        {
            return _context.Asistencias.Any(e => e.IdAsistencia == id);
        }
    }
}
