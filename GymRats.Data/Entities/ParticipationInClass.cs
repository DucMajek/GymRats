using System;
using System.Collections.Generic;

namespace GymRats.Data.Entities;

public partial class ParticipationInClass
{
    public int IdParticipation { get; set; }

    public int IdGroup { get; set; }

    public int IdUser { get; set; }

    public virtual GroupClass IdGroupNavigation { get; set; } = null!;

    public virtual User IdUserNavigation { get; set; } = null!;
}
