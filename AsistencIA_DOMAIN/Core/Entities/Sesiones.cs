using System;
using System.Collections.Generic;
using AsistencIA_DOMAIN.Core.Entities;

namespace AsistencIA_DOMAIN.Data;

public partial class Sesiones
{
    public int IdSesion { get; set; }

    public int IdSeccion { get; set; }

    public DateOnly Fecha { get; set; }

    public TimeOnly HoraInicio { get; set; }

    public string? ImagenUrl { get; set; }

    public string? Estado { get; set; }

    public string TipoIngreso { get; set; } = null!;

    public virtual ICollection<Asistencias> Asistencias { get; set; } = new List<Asistencias>();

    public virtual Secciones IdSeccionNavigation { get; set; } = null!;
}
