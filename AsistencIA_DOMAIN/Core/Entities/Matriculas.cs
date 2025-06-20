using System;
using System.Collections.Generic;
using AsistencIA_DOMAIN.Core.Entities;

namespace AsistencIA_DOMAIN.Data;

public partial class Matriculas
{
    public string IdUsuario { get; set; } = null!;
    public int IdSeccion { get; set; }

    // Relaciones de navegación
    public virtual Usuarios? Usuario { get; set; }
    public virtual Secciones? Seccion { get; set; }
}
