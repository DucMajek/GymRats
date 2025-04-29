using System;
using System.Collections.Generic;

namespace GymRats.Data.Entities;

public partial class User
{
    public int IdUser { get; set; }

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int IdRole { get; set; }

    public virtual Role IdRoleNavigation { get; set; } = null!;

    public virtual Person IdUserNavigation { get; set; } = null!;

    public virtual ICollection<ParticipationInClass> ParticipationInClasses { get; set; } = new List<ParticipationInClass>();

    public virtual ICollection<PersonalTraining> PersonalTrainings { get; set; } = new List<PersonalTraining>();

    public virtual ICollection<PurchasedCourse> PurchasedCourses { get; set; } = new List<PurchasedCourse>();

    public virtual ICollection<UserEbook> UserEbooks { get; set; } = new List<UserEbook>();

    public virtual ICollection<UserPass> UserPasses { get; set; } = new List<UserPass>();

    public virtual ICollection<UserTrainingPlan> UserTrainingPlans { get; set; } = new List<UserTrainingPlan>();
}
