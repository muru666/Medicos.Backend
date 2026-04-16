using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Medicos.Backend.Data.Models;

public partial class Especialidad
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<Medico> Medicos { get; set; } = new List<Medico>();
}
