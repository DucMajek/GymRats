using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GymRats.Server.Models;

[Table("Uzytkownik_Jadlospis")]
public partial class UzytkownikJadlospi
{
    [Key]
    [Column("id_uzytkownik_jadlospis")]
    public int IdUzytkownikJadlospis { get; set; }

    [Column("Uzytkownik_id_uzytkownika")]
    public int UzytkownikIdUzytkownika { get; set; }

    [Column("Jadlospis_id_jadlospisu")]
    public int JadlospisIdJadlospisu { get; set; }

    [ForeignKey("JadlospisIdJadlospisu")]
    [InverseProperty("UzytkownikJadlospis")]
    public virtual Jadlospi JadlospisIdJadlospisuNavigation { get; set; } = null!;

    [ForeignKey("UzytkownikIdUzytkownika")]
    [InverseProperty("UzytkownikJadlospis")]
    public virtual Uzytkownik UzytkownikIdUzytkownikaNavigation { get; set; } = null!;
}
