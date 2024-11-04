using System;
using System.Collections.Generic;

namespace gymrats.server.Models;

public partial class Karnet
{
    public int IdKarnet { get; set; }

    public DateOnly DataWydania { get; set; }

    public DateOnly DataWygasniecia { get; set; }

    public int TypKarnetuIdTypKarnetu { get; set; }

    public int UzytkownikIdUzytkownika { get; set; }

    public virtual TypKarnetu TypKarnetuIdTypKarnetuNavigation { get; set; } = null!;

    public virtual Uzytkownik UzytkownikIdUzytkownikaNavigation { get; set; } = null!;
}
