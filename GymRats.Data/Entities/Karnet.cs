using System;
using System.Collections.Generic;

namespace GymRats.Data.Entities;

public partial class Karnet
{
    public int IdKarnet { get; set; }

    public DateOnly? StartKarnetu { get; set; }

    public DateOnly? KoniecUmowy { get; set; }

    public int Aktywny { get; set; }

    public int TypKarnetuIdTypKarnetu { get; set; }

    public int UzytkownikIdUzytkownik { get; set; }

    public virtual TypKarnetu TypKarnetuIdTypKarnetuNavigation { get; set; } = null!;

    public virtual Uzytkownik UzytkownikIdUzytkownikNavigation { get; set; } = null!;
}
