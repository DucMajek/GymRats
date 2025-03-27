using System;
using System.Collections.Generic;

namespace GymRats.Data.Entities;

public partial class Osoba
{
    public int IdOsoba { get; set; }

    public string Imie { get; set; } = null!;

    public string Nazwisko { get; set; } = null!;

    public DateTime? DataUrodzenia { get; set; }

    public string Adres { get; set; } = null!;

    public string NrTel { get; set; } = null!;

    public string Plec { get; set; } = null!;

    public float Waga { get; set; }

    public int Wzrost { get; set; }

    public virtual ICollection<Trener> Treners { get; set; } = new List<Trener>();

    public virtual ICollection<Uzytkownik> Uzytkowniks { get; set; } = new List<Uzytkownik>();
}
