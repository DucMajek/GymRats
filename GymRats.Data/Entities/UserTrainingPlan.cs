using System;
using System.Collections.Generic;

namespace GymRats.Data.Entities;

public partial class UserTrainingPlan
{
    public int IdUserTrainingPlan { get; set; }

    public int IdTrainingPlan { get; set; }

    public int IdUser { get; set; }

    public virtual TrainingPlan IdTrainingPlanNavigation { get; set; } = null!;

    public virtual User IdUserNavigation { get; set; } = null!;
}
