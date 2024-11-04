using System;
using System.Collections.Generic;

namespace gymrats.server.Models;

public partial class UzytkownikBlog
{
    public int IdUzytkownikBlog { get; set; }

    public int UzytkownikIdUzytkownika { get; set; }

    public int BlogIdBlogu { get; set; }

    public virtual Blog BlogIdBloguNavigation { get; set; } = null!;

    public virtual Uzytkownik UzytkownikIdUzytkownikaNavigation { get; set; } = null!;
}
