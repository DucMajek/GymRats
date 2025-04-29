using System;
using System.Collections.Generic;

namespace GymRats.Data.Entities;

public partial class Coach
{
    public int IdCoach { get; set; }

    public string Specialization { get; set; } = null!;

    public virtual ICollection<GroupClass> GroupClasses { get; set; } = new List<GroupClass>();

    public virtual Person IdCoachNavigation { get; set; } = null!;

    public virtual ICollection<PersonalTraining> PersonalTrainings { get; set; } = new List<PersonalTraining>();

    public virtual ICollection<TrainerCourse> TrainerCourses { get; set; } = new List<TrainerCourse>();
}
