using System;
using System.Collections.Generic;

namespace GymRats.Data.Entities;

public partial class UdzialWZajeciach
{
    public int IdUdzial { get; set; }

    public int ZajeciaGrupoweIdZajec { get; set; }

    public int UzytkownikIdUzytkownik { get; set; }

    public virtual Uzytkownik UzytkownikIdUzytkownikNavigation { get; set; } = null!;

    public virtual ZajeciaGrupowe ZajeciaGrupoweIdZajecNavigation { get; set; } = null!;
}
