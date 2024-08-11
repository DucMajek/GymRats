using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GymRats.Server.Models;

[Table("Plan_treningowy_Uzytkownik")]
public partial class PlanTreningowyUzytkownik
{
    [Key]
    [Column("id_plan_treningowy")]
    public int IdPlanTreningowy { get; set; }

    [Column("Uzytkownik_id_uzytkownika")]
    public int UzytkownikIdUzytkownika { get; set; }

    [Column("Plan_treningowy_id_plan_treningowy")]
    public int PlanTreningowyIdPlanTreningowy { get; set; }

    [ForeignKey("PlanTreningowyIdPlanTreningowy")]
    [InverseProperty("PlanTreningowyUzytkowniks")]
    public virtual PlanTreningowy PlanTreningowyIdPlanTreningowyNavigation { get; set; } = null!;

    [ForeignKey("UzytkownikIdUzytkownika")]
    [InverseProperty("PlanTreningowyUzytkowniks")]
    public virtual Uzytkownik UzytkownikIdUzytkownikaNavigation { get; set; } = null!;
}
