using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GymRats.Server.Models;

[Table("Trening")]
public partial class Trening
{
    [Key]
    [Column("id_trening")]
    public int IdTrening { get; set; }

    [Column("nazwa_cwiczenia")]
    [Unicode(false)]
    public string NazwaCwiczenia { get; set; } = null!;

    [Column("ilosc_cwiczen")]
    public int IloscCwiczen { get; set; }

    [Column("dzien_tygodnia")]
    [StringLength(12)]
    [Unicode(false)]
    public string DzienTygodnia { get; set; } = null!;

    [Column("potworzenia_na_serie")]
    [StringLength(5)]
    [Unicode(false)]
    public string PotworzeniaNaSerie { get; set; } = null!;

    [InverseProperty("TreningIdTreningNavigation")]
    public virtual ICollection<PlanTreningowy> PlanTreningowies { get; set; } = new List<PlanTreningowy>();
}
