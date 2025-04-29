using System;
using System.Collections.Generic;

namespace GymRats.Data.Entities;

public partial class PersonalTraining
{
    public int IdPersonalTraining { get; set; }

    public DateTime? ReservationDateTime { get; set; }

    public int IdCoach { get; set; }

    public int IdUser { get; set; }

    public virtual Coach IdCoachNavigation { get; set; } = null!;

    public virtual User IdUserNavigation { get; set; } = null!;
}
