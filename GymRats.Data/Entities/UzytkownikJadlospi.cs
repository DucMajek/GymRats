using System;
using System.Collections.Generic;

namespace GymRats.Data.Entities;

public partial class UzytkownikJadlospi
{
    public int IdUzytkownikJadlospis { get; set; }

    public int JadlospisIdJadlospisu { get; set; }

    public int UzytkownikIdUzytkownik { get; set; }

    public virtual Jadlospi JadlospisIdJadlospisuNavigation { get; set; } = null!;

    public virtual Uzytkownik UzytkownikIdUzytkownikNavigation { get; set; } = null!;
}
