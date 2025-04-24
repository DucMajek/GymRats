using System;
using System.Collections.Generic;

namespace GymRats.Data.Entities;

public partial class Osoba
{
    public int IdOsoba { get; set; }

    public string Imie { get; set; } = null!;

    public string Nazwisko { get; set; } = null!;

    public DateOnly? DataUrodzenia { get; set; }

    public string NrTel { get; set; } = null!;

    public string Plec { get; set; } = null!;

    public decimal Waga { get; set; }

    public int Wzrost { get; set; }

    public string? Adres { get; set; }

    public string? NumerBudynku { get; set; }

    public string? KodPocztowy { get; set; }

    public string? Miejscowosc { get; set; }

    public virtual Trener? Trener { get; set; }

    public virtual Uzytkownik? Uzytkownik { get; set; }
}
