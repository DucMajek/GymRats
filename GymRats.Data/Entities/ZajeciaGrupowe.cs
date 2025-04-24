using System;
using System.Collections.Generic;

namespace GymRats.Data.Entities;

public partial class ZajeciaGrupowe
{
    public int IdZajec { get; set; }

    public string NazwaZajec { get; set; } = null!;

    public DateTime? Data { get; set; }

    public int TrenerIdTrener { get; set; }

    public virtual Trener TrenerIdTrenerNavigation { get; set; } = null!;

    public virtual ICollection<UdzialWZajeciach> UdzialWZajeciaches { get; set; } = new List<UdzialWZajeciach>();
}
