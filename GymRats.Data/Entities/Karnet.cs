using System;
using System.Collections.Generic;

namespace GymRats.Data.Entities;

public partial class Karnet
{
    public int IdKarnet { get; set; }

    public DateOnly startKarnetu { get; set; }

    public DateOnly koniecUmowy { get; set; }

    public int TypKarnetuIdTypKarnetu { get; set; }

    public int UzytkownikIdUzytkownika { get; set; }

    public virtual TypKarnetu TypKarnetuIdTypKarnetuNavigation { get; set; } = null!;

    public virtual Uzytkownik UzytkownikIdUzytkownikaNavigation { get; set; } = null!;
}
