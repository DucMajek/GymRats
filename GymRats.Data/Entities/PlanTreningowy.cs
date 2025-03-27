using System;
using System.Collections.Generic;

namespace GymRats.Data.Entities;

public partial class PlanTreningowy
{
    public int IdPlanTreningowy { get; set; }

    public string NazwaPlanu { get; set; } = null!;

    public string Poziom { get; set; } = null!;

    public byte[] ZawartoscPlanuTreningowego { get; set; } = null!;

    public int TrenerIdTrenera { get; set; }

    public int TreningIdTrening { get; set; }

    public virtual ICollection<Ankietum> Ankieta { get; set; } = new List<Ankietum>();

    public virtual ICollection<PlanTreningowyUzytkownik> PlanTreningowyUzytkowniks { get; set; } = new List<PlanTreningowyUzytkownik>();

    public virtual Trener TrenerIdTreneraNavigation { get; set; } = null!;

    public virtual Trening TreningIdTreningNavigation { get; set; } = null!;
}
