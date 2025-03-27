using System;
using System.Collections.Generic;

namespace GymRats.Data.Entities;

public partial class UzytkownikJadlospi
{
    public int IdUzytkownikJadlospis { get; set; }

    public int UzytkownikIdUzytkownika { get; set; }

    public int JadlospisIdJadlospisu { get; set; }

    public virtual Jadlospi JadlospisIdJadlospisuNavigation { get; set; } = null!;

    public virtual Uzytkownik UzytkownikIdUzytkownikaNavigation { get; set; } = null!;
}
