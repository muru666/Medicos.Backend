using System;
using System.Collections.Generic;

namespace Medicos.Backend.Data.Models;

public partial class Medico
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public int Especialidad { get; set; }

    public virtual Especialidad EspecialidadNavigation { get; set; } = null!;
}
