using System;
using System.Collections.Generic;

namespace GymRats.Data.Entities;

public partial class FoodEbook
{
    public int IdEbook { get; set; }

    public string Calories { get; set; } = null!;

    public string DietType { get; set; } = null!;

    public byte[] EbookFile { get; set; } = null!;

    public virtual ICollection<UserEbook> UserEbooks { get; set; } = new List<UserEbook>();
}
