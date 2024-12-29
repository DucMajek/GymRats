using System;
using System.Collections.Generic;

namespace gymrats.server.Models;

public partial class TypKarnetu
{
    public int IdTypKarnetu { get; set; }

    public string Nazwa { get; set; } = null!;

    public float Cena { get; set; }

    public string? Opis { get; set; }

    public virtual ICollection<Karnet> Karnets { get; set; } = new List<Karnet>();
}
