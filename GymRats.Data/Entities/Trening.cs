using System;
using System.Collections.Generic;

namespace GymRats.Data.Entities;

public partial class Trening
{
    public int IdTrening { get; set; }

    public string NazwaCwiczenia { get; set; } = null!;

    public int IloscCwiczen { get; set; }

    public string DzienTygodnia { get; set; } = null!;

    public string PowtorzeniaNaSerie { get; set; } = null!;

    public virtual ICollection<PlanTreningowy> PlanTreningowies { get; set; } = new List<PlanTreningowy>();
}
