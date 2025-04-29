using System;
using System.Collections.Generic;

namespace GymRats.Data.Entities;

public partial class GroupClass
{
    public int IdGroup { get; set; }

    public string ClassType { get; set; } = null!;

    public DateTime? StartDate { get; set; }

    public int GroupSize { get; set; }

    public int IdCoach { get; set; }

    public virtual Coach IdCoachNavigation { get; set; } = null!;

    public virtual ICollection<ParticipationInClass> ParticipationInClasses { get; set; } = new List<ParticipationInClass>();
}
