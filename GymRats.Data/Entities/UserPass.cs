using System;
using System.Collections.Generic;

namespace GymRats.Data.Entities;

public partial class UserPass
{
    public int IdPass { get; set; }

    public DateOnly? DateStart { get; set; }

    public DateOnly? DateEnd { get; set; }

    public int IdTypePass { get; set; }

    public int IdUser { get; set; }

    public int IdStatus { get; set; }

    public virtual PassStatus IdStatusNavigation { get; set; } = null!;

    public virtual TypePass IdTypePassNavigation { get; set; } = null!;

    public virtual User IdUserNavigation { get; set; } = null!;
}
