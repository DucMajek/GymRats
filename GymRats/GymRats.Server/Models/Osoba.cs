using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GymRats.Server.Models;

[Table("Osoba")]
public partial class Osoba
{
    [Key]
    [Column("id_osoba")]
    public int IdOsoba { get; set; }

    [StringLength(15)]
    [Unicode(false)]
    public string Imie { get; set; } = null!;

    [StringLength(15)]
    [Unicode(false)]
    public string Nazwisko { get; set; } = null!;

    [Column("Data_urodzenia", TypeName = "datetime")]
    public DateTime DataUrodzenia { get; set; }

    [StringLength(25)]
    [Unicode(false)]
    public string Adres { get; set; } = null!;

    [Column("nr_tel")]
    [StringLength(12)]
    [Unicode(false)]
    public string NrTel { get; set; } = null!;

    [Column("plec")]
    [StringLength(10)]
    [Unicode(false)]
    public string Plec { get; set; } = null!;

    [Column("BMI")]
    public float Bmi { get; set; }

    [Column("waga")]
    public float Waga { get; set; }

    [Column("wzrost")]
    public int Wzrost { get; set; }

    [InverseProperty("OsobaIdOsobaNavigation")]
    public virtual ICollection<Trener> Treners { get; set; } = new List<Trener>();

    [InverseProperty("OsobaIdOsobaNavigation")]
    public virtual ICollection<Uzytkownik> Uzytkowniks { get; set; } = new List<Uzytkownik>();
}
