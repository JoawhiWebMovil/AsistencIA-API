using AsistencIA_DOMAIN.Data;
using System;
using System.Collections.Generic;

namespace AsistencIA_DOMAIN.Core.Entities;

public partial class Asistencias
{
    public int IdAsistencia { get; set; }

    public int IdSesion { get; set; }

    public string IdUsuario { get; set; }

    public string Estado { get; set; } = null!;

    public DateTime? Timestamp { get; set; }

    public virtual Sesiones IdSesionNavigation { get; set; } = null!;

    public virtual Usuarios IdUsuarioNavigation { get; set; } = null!;
}
