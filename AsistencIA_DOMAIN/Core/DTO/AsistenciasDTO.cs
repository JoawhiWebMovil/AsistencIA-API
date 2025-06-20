using AsistencIA_DOMAIN.Core.Entities;
using AsistencIA_DOMAIN.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsistencIA_DOMAIN.Core.DTOs
{

    public class AsistenciaUpdateRequest
    {
        public int IdSesion { get; set; }
        public string IdUsuario { get; set; } = string.Empty;
        public string NuevoEstado { get; set; } = string.Empty;
    }

    public class AsistenciaCreateRequest
    {
        public int IdSesion { get; set; }
        public string IdUsuario { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;
    }

}
