using System;
using System.Collections.Generic;

namespace GymRats.Data.Entities;

public partial class TypePass
{
    public int IdTypePass { get; set; }

    public string GymPassName { get; set; } = null!;

    public int Price { get; set; }

    public int DurationPass { get; set; }

    public string Description { get; set; } = null!;

    public virtual ICollection<UserPass> UserPasses { get; set; } = new List<UserPass>();
}
