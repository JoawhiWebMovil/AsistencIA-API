using AsistencIA_DOMAIN.Core.Entities;
using AsistencIA_DOMAIN.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsistencIA_DOMAIN.Core.DTOs
{
    
    public class UsuariosResponseAuthDTO
    {
        public string IdUsuario { get; set; }
        public string Nombre { get; set; } = null!;
        public string Apellidos { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Contrasena { get; set; } = null!;
        public string Rol { get; set; } = null!;
        public string? FotoReferencia { get; set; }
        public DateOnly? FechaNacimiento { get; set; }
        public string? Token { get; set; }

    }


    public partial class UsuariosRequestChangePasswordDTO
    {
        public string Usuario { get; set; }
        public string Contrasena { get; set; } = null!;
        public string NewContrasena { get; set; } = null!;
    }

    public partial class UsuariosAuthDTO
    {
        public string Usuario { get; set; }
        public string Contrasena { get; set; } = null!;
    }

}
