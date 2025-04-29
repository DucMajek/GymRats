using System;
using System.Collections.Generic;

namespace GymRats.Data.Entities;

public partial class UserEbook
{
    public int IdUserEbook { get; set; }

    public int IdUser { get; set; }

    public int IdEbook { get; set; }

    public virtual FoodEbook IdEbookNavigation { get; set; } = null!;

    public virtual User IdUserNavigation { get; set; } = null!;
}
