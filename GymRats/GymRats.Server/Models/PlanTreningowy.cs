using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GymRats.Server.Models;

[Table("Plan_treningowy")]
public partial class PlanTreningowy
{
    [Key]
    [Column("id_plan_treningowy")]
    public int IdPlanTreningowy { get; set; }

    [Column("nazwa_planu")]
    [StringLength(30)]
    [Unicode(false)]
    public string NazwaPlanu { get; set; } = null!;

    [Column("poziom")]
    [StringLength(50)]
    [Unicode(false)]
    public string Poziom { get; set; } = null!;

    [Column("zawartosc_planu_treningowego")]
    public byte[] ZawartoscPlanuTreningowego { get; set; } = null!;

    [Column("Trener_id_trenera")]
    public int TrenerIdTrenera { get; set; }

    [Column("Trening_id_trening")]
    public int TreningIdTrening { get; set; }

    [InverseProperty("PlanTreningowyIdPlanTreningowyNavigation")]
    public virtual ICollection<Ankietum> Ankieta { get; set; } = new List<Ankietum>();

    [InverseProperty("PlanTreningowyIdPlanTreningowyNavigation")]
    public virtual ICollection<PlanTreningowyUzytkownik> PlanTreningowyUzytkowniks { get; set; } = new List<PlanTreningowyUzytkownik>();

    [ForeignKey("TrenerIdTrenera")]
    [InverseProperty("PlanTreningowies")]
    public virtual Trener TrenerIdTreneraNavigation { get; set; } = null!;

    [ForeignKey("TreningIdTrening")]
    [InverseProperty("PlanTreningowies")]
    public virtual Trening TreningIdTreningNavigation { get; set; } = null!;
}
