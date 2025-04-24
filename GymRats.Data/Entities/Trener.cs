using System;
using System.Collections.Generic;

namespace GymRats.Data.Entities;

public partial class Trener
{
    public int IdTrener { get; set; }

    public string Specjalizacja { get; set; } = null!;

    public virtual Osoba IdTrenerNavigation { get; set; } = null!;

    public virtual ICollection<KursTrenera> KursTreneras { get; set; } = new List<KursTrenera>();

    public virtual ICollection<TreningPersonalny> TreningPersonalnies { get; set; } = new List<TreningPersonalny>();

    public virtual ICollection<ZajeciaGrupowe> ZajeciaGrupowes { get; set; } = new List<ZajeciaGrupowe>();
}
