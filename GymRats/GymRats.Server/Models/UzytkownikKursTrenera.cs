using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GymRats.Server.Models;

[Table("Uzytkownik_Kurs_Trenera")]
public partial class UzytkownikKursTrenera
{
    [Key]
    [Column("id_uzytkownik_kurs_trenera")]
    public int IdUzytkownikKursTrenera { get; set; }

    [Column("Kurs_Trenera_id_kursu")]
    public int KursTreneraIdKursu { get; set; }

    [Column("Uzytkownik_id_uzytkownika")]
    public int UzytkownikIdUzytkownika { get; set; }

    [ForeignKey("KursTreneraIdKursu")]
    [InverseProperty("UzytkownikKursTreneras")]
    public virtual KursTrenera KursTreneraIdKursuNavigation { get; set; } = null!;

    [ForeignKey("UzytkownikIdUzytkownika")]
    [InverseProperty("UzytkownikKursTreneras")]
    public virtual Uzytkownik UzytkownikIdUzytkownikaNavigation { get; set; } = null!;
}
