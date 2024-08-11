using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GymRats.Server.Models;

public partial class Jadlospi
{
    [Key]
    [Column("id_jadlospisu")]
    public int IdJadlospisu { get; set; }

    [Column("kalorycznosc")]
    [StringLength(50)]
    [Unicode(false)]
    public string Kalorycznosc { get; set; } = null!;

    [Column("rodzaj_diety")]
    [StringLength(50)]
    [Unicode(false)]
    public string RodzajDiety { get; set; } = null!;

    [Column("plec")]
    [StringLength(20)]
    [Unicode(false)]
    public string Plec { get; set; } = null!;

    [Column("zawartosc_jadlospisu")]
    public byte[] ZawartoscJadlospisu { get; set; } = null!;

    [InverseProperty("JadlospisIdJadlospisuNavigation")]
    public virtual ICollection<UzytkownikJadlospi> UzytkownikJadlospis { get; set; } = new List<UzytkownikJadlospi>();
}
