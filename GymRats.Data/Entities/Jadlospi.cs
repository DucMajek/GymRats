using System;
using System.Collections.Generic;

namespace GymRats.Data.Entities;

public partial class Jadlospi
{
    public int IdJadlospisu { get; set; }

    public string Kalorycznosc { get; set; } = null!;

    public string RodzajDiety { get; set; } = null!;

    public byte[] ZawartoscJadlospisu { get; set; } = null!;

    public virtual ICollection<UzytkownikJadlospi> UzytkownikJadlospis { get; set; } = new List<UzytkownikJadlospi>();
}
