using System;
using System.Collections.Generic;

namespace GymRats.Data.Entities;

public partial class Uzytkownik
{
    public int IdUzytkownik { get; set; }

    public string Email { get; set; } = null!;

    public string Haslo { get; set; } = null!;

    public virtual Osoba IdUzytkownikNavigation { get; set; } = null!;

    public virtual ICollection<Karnet> Karnets { get; set; } = new List<Karnet>();

    public virtual ICollection<PlanTreningowyUzytkownik> PlanTreningowyUzytkowniks { get; set; } = new List<PlanTreningowyUzytkownik>();

    public virtual ICollection<TreningPersonalny> TreningPersonalnies { get; set; } = new List<TreningPersonalny>();

    public virtual ICollection<UdzialWZajeciach> UdzialWZajeciaches { get; set; } = new List<UdzialWZajeciach>();

    public virtual ICollection<UzytkownikJadlospi> UzytkownikJadlospis { get; set; } = new List<UzytkownikJadlospi>();

    public virtual ICollection<UzytkownikKursTrenera> UzytkownikKursTreneras { get; set; } = new List<UzytkownikKursTrenera>();
}
