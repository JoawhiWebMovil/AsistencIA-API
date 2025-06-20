using System;
using System.Collections.Generic;
using AsistencIA_DOMAIN.Core.Entities;

namespace AsistencIA_DOMAIN.Data;

public partial class Secciones
{
    public int IdSeccion { get; set; }

    public int IdCurso { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual Cursos IdCursoNavigation { get; set; } = null!;

    public virtual ICollection<Sesiones> Sesiones { get; set; } = new List<Sesiones>();

    public virtual ICollection<Usuarios> IdUsuario { get; set; } = new List<Usuarios>();

    public virtual ICollection<Matriculas> Matriculas { get; set; } = new List<Matriculas>();

}
