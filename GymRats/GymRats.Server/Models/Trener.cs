using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GymRats.Server.Models;

[Table("Trener")]
public partial class Trener
{
    [Column("Osoba_id_osoba")]
    public int OsobaIdOsoba { get; set; }

    [Key]
    [Column("id_trenera")]
    public int IdTrenera { get; set; }

    [Column("specjalizacja")]
    [StringLength(15)]
    [Unicode(false)]
    public string Specjalizacja { get; set; } = null!;

    [Column("doswiadczenie")]
    [StringLength(50)]
    [Unicode(false)]
    public string Doswiadczenie { get; set; } = null!;

    [InverseProperty("TrenerIdTreneraNavigation")]
    public virtual ICollection<KursTrenera> KursTreneras { get; set; } = new List<KursTrenera>();

    [ForeignKey("OsobaIdOsoba")]
    [InverseProperty("Treners")]
    public virtual Osoba OsobaIdOsobaNavigation { get; set; } = null!;

    [InverseProperty("TrenerIdTreneraNavigation")]
    public virtual ICollection<PlanTreningowy> PlanTreningowies { get; set; } = new List<PlanTreningowy>();
}
