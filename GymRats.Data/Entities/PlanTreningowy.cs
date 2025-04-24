using System;
using System.Collections.Generic;

namespace GymRats.Data.Entities;

public partial class PlanTreningowy
{
    public int IdPlanTreningowy { get; set; }

    public string NazwaPlanu { get; set; } = null!;

    public byte[] ZawartoscPlanuTreningowego { get; set; } = null!;

    public virtual ICollection<PlanTreningowyUzytkownik> PlanTreningowyUzytkowniks { get; set; } = new List<PlanTreningowyUzytkownik>();
}
