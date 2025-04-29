using System;
using System.Collections.Generic;

namespace GymRats.Data.Entities;

public partial class PassStatus
{
    public int IdStatus { get; set; }

    public string StatusType { get; set; } = null!;

    public virtual ICollection<UserPass> UserPasses { get; set; } = new List<UserPass>();
}
