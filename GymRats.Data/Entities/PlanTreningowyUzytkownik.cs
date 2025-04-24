using System;
using System.Collections.Generic;

namespace GymRats.Data.Entities;

public partial class PlanTreningowyUzytkownik
{
    public int IdPlanTreningowy { get; set; }

    public int PlanTreningowyIdPlanTreningowy { get; set; }

    public int UzytkownikIdUzytkownik { get; set; }

    public virtual PlanTreningowy PlanTreningowyIdPlanTreningowyNavigation { get; set; } = null!;

    public virtual Uzytkownik UzytkownikIdUzytkownikNavigation { get; set; } = null!;
}
