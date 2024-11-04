using System;
using System.Collections.Generic;

namespace gymrats.server.Models;

public partial class KursTrenera
{
    public int IdKursu { get; set; }

    public string Nazwa { get; set; } = null!;

    public string CzasTrwania { get; set; } = null!;

    public int TrenerIdTrenera { get; set; }

    public virtual Trener TrenerIdTreneraNavigation { get; set; } = null!;

    public virtual ICollection<UzytkownikKursTrenera> UzytkownikKursTreneras { get; set; } = new List<UzytkownikKursTrenera>();
}
