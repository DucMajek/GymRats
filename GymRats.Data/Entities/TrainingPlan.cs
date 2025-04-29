using System;
using System.Collections.Generic;

namespace GymRats.Data.Entities;

public partial class TrainingPlan
{
    public int IdTrainingPlan { get; set; }

    public string TrainingPlanName { get; set; } = null!;

    public byte[] TrainingPlanFile { get; set; } = null!;

    public virtual ICollection<UserTrainingPlan> UserTrainingPlans { get; set; } = new List<UserTrainingPlan>();
}
