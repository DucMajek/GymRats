using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GymRats.Server.Models;

[Table("Karnet")]
public partial class Karnet
{
    [Key]
    [Column("id_karnet")]
    public int IdKarnet { get; set; }

    [Column("data_wydania", TypeName = "date")]
    public DateTime DataWydania { get; set; }

    [Column("data_wygasniecia", TypeName = "date")]
    public DateTime DataWygasniecia { get; set; }

    [Column("Typ_Karnetu_id_typ_karnetu")]
    public int TypKarnetuIdTypKarnetu { get; set; }

    [Column("Uzytkownik_id_uzytkownika")]
    public int UzytkownikIdUzytkownika { get; set; }

    [ForeignKey("TypKarnetuIdTypKarnetu")]
    [InverseProperty("Karnets")]
    public virtual TypKarnetu TypKarnetuIdTypKarnetuNavigation { get; set; } = null!;

    [ForeignKey("UzytkownikIdUzytkownika")]
    [InverseProperty("Karnets")]
    public virtual Uzytkownik UzytkownikIdUzytkownikaNavigation { get; set; } = null!;
}
