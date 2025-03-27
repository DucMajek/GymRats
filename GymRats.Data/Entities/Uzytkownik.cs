using System;
using System.Collections.Generic;

namespace GymRats.Data.Entities;

public partial class Uzytkownik
{
    public int IdUzytkownika { get; set; }

    public int OsobaIdOsoba { get; set; }

    public string Email { get; set; } = null!;

    public string Haslo { get; set; } = null!;

    public virtual ICollection<Ankietum> Ankieta { get; set; } = new List<Ankietum>();

    public virtual ICollection<Karnet> Karnets { get; set; } = new List<Karnet>();

    public virtual Osoba OsobaIdOsobaNavigation { get; set; } = null!;

    public virtual ICollection<PlanTreningowyUzytkownik> PlanTreningowyUzytkowniks { get; set; } = new List<PlanTreningowyUzytkownik>();

    public virtual ICollection<UzytkownikBlog> UzytkownikBlogs { get; set; } = new List<UzytkownikBlog>();

    public virtual ICollection<UzytkownikJadlospi> UzytkownikJadlospis { get; set; } = new List<UzytkownikJadlospi>();

    public virtual ICollection<UzytkownikKursTrenera> UzytkownikKursTreneras { get; set; } = new List<UzytkownikKursTrenera>();
}
