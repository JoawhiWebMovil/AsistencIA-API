using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using AsistencIA_DOMAIN.Core.Entities;

namespace AsistencIA_DOMAIN.Data;

public partial class Usuarios
{
    public string IdUsuario { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellidos { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Contrasena { get; set; } = null!;

    public string Rol { get; set; } = null!;

    public string? FotoReferencia { get; set; }

    [Column("fecha_nacimiento")]
    public DateOnly? FechaNacimiento { get; set; }

    public bool Estado { get; set; }

    public virtual ICollection<Asistencias> Asistencias { get; set; } = new List<Asistencias>();

    public virtual ICollection<Secciones> IdSeccion { get; set; } = new List<Secciones>();

    public virtual ICollection<Matriculas> Matriculas { get; set; } = new List<Matriculas>();

}
