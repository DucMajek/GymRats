using System;
using System.Collections.Generic;

namespace GymRats.Data.Entities;

public partial class PurchasedCourse
{
    public int IdPurchasedCourse { get; set; }

    public int IdCourse { get; set; }

    public int IdUser { get; set; }

    public virtual TrainerCourse IdCourseNavigation { get; set; } = null!;

    public virtual User IdUserNavigation { get; set; } = null!;
}
