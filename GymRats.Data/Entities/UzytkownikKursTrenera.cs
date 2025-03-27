using System;
using System.Collections.Generic;

namespace GymRats.Data.Entities;

public partial class UzytkownikKursTrenera
{
    public int IdUzytkownikKursTrenera { get; set; }

    public int KursTreneraIdKursu { get; set; }

    public int UzytkownikIdUzytkownika { get; set; }

    public virtual KursTrenera KursTreneraIdKursuNavigation { get; set; } = null!;

    public virtual Uzytkownik UzytkownikIdUzytkownikaNavigation { get; set; } = null!;
}
