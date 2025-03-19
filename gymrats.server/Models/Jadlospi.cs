using System;
using System.Collections.Generic;

namespace gymrats.server.Models;

public partial class Jadlospi
{
    public int IdJadlospisu { get; set; }

    public string Kalorycznosc { get; set; } = null!;

    public string RodzajDiety { get; set; } = null!;
    
    public byte[] ZawartoscJadlospisu { get; set; } = null!;

    public virtual ICollection<UzytkownikJadlospi> UzytkownikJadlospis { get; set; } = new List<UzytkownikJadlospi>();
}
