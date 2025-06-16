using System;
using System.Collections.Generic;
using AsistencIA_DOMAIN.Data;

namespace AsistencIA_DOMAIN.Core.Entities;

public partial class Cursos
{
    public int IdCurso { get; set; }

    public string Nombre { get; set; } = null!;

    public string Codigo { get; set; } = null!;

    public virtual ICollection<Secciones> Secciones { get; set; } = new List<Secciones>();
}
