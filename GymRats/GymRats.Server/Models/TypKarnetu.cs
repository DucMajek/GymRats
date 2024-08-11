using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GymRats.Server.Models;

[Table("Typ_Karnetu")]
public partial class TypKarnetu
{
    [Key]
    [Column("id_typ_karnetu")]
    public int IdTypKarnetu { get; set; }

    [Column("nazwa")]
    [StringLength(30)]
    [Unicode(false)]
    public string Nazwa { get; set; } = null!;

    [Column("cena")]
    public float Cena { get; set; }

    [InverseProperty("TypKarnetuIdTypKarnetuNavigation")]
    public virtual ICollection<Karnet> Karnets { get; set; } = new List<Karnet>();
}
