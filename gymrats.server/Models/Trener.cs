using System;
using System.Collections.Generic;

namespace gymrats.server.Models;

public partial class Trener
{
    public int OsobaIdOsoba { get; set; }

    public int IdTrenera { get; set; }

    public string Specjalizacja { get; set; } = null!;

    public string Doswiadczenie { get; set; } = null!;

    public virtual ICollection<KursTrenera> KursTreneras { get; set; } = new List<KursTrenera>();

    public virtual Osoba OsobaIdOsobaNavigation { get; set; } = null!;

    public virtual ICollection<PlanTreningowy> PlanTreningowies { get; set; } = new List<PlanTreningowy>();
}
