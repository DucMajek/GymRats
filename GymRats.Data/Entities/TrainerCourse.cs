using System;
using System.Collections.Generic;

namespace GymRats.Data.Entities;

public partial class TrainerCourse
{
    public int IdCourse { get; set; }

    public string CourseName { get; set; } = null!;

    public string Duration { get; set; } = null!;

    public string Description { get; set; } = null!;

    public int IdCoach { get; set; }

    public virtual Coach IdCoachNavigation { get; set; } = null!;

    public virtual ICollection<PurchasedCourse> PurchasedCourses { get; set; } = new List<PurchasedCourse>();
}
