using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AsistencIA_DOMAIN.Data;
using AsistencIA_DOMAIN.Core.DTOs;
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

        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePwd([FromBody] UsuariosRequestChangePasswordDTO userChangPwdDTO)
        {
            if (userChangPwdDTO.Usuario == "" || userChangPwdDTO.Contrasena == "" || userChangPwdDTO.NewContrasena == "") return BadRequest();

            var result = await _usuariosService.ChangePwd(userChangPwdDTO.Usuario, userChangPwdDTO.Contrasena, userChangPwdDTO.NewContrasena);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpGet("secciones/{idDocente}")]
        public IActionResult GetSeccionesPorDocente(string idDocente)
        {
            var resultado = (from m in _context.Matriculas
                             join s in _context.Secciones on m.IdSeccion equals s.IdSeccion
                             join c in _context.Cursos on s.IdCurso equals c.IdCurso
                             join u in _context.Usuarios on m.IdUsuario equals u.IdUsuario
                             where u.Rol == "docente" && u.IdUsuario == idDocente
                             select new
                             {
                                 Nombre = c.Nombre + " | " + s.Nombre,
                                 seccion = s.IdSeccion
                             }).ToList();

            return Ok(resultado);
        }

        private bool UsuariosExists(string id)
        {
            return _context.Usuarios.Any(e => e.IdUsuario == id);
        }
    }
}
