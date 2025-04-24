using System;
using System.Collections.Generic;

namespace GymRats.Data.Entities;

public partial class TreningPersonalny
{
    public int IdTreninguPersonalnego { get; set; }

    public DateOnly? DataOd { get; set; }

    public DateOnly? DataDo { get; set; }

    public int UzytkownikIdUzytkownik { get; set; }

    public int TrenerIdTrener { get; set; }

    public virtual Trener TrenerIdTrenerNavigation { get; set; } = null!;

    public virtual Uzytkownik UzytkownikIdUzytkownikNavigation { get; set; } = null!;
}
