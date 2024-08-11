using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GymRats.Server.Models;

[Table("Uzytkownik")]
public partial class Uzytkownik
{
    [Key]
    [Column("id_uzytkownika")]
    public int IdUzytkownika { get; set; }

    [Column("Osoba_id_osoba")]
    public int OsobaIdOsoba { get; set; }

    [InverseProperty("UzytkownikIdUzytkownikaNavigation")]
    public virtual ICollection<Ankietum> Ankieta { get; set; } = new List<Ankietum>();

    [InverseProperty("UzytkownikIdUzytkownikaNavigation")]
    public virtual ICollection<Karnet> Karnets { get; set; } = new List<Karnet>();

    [ForeignKey("OsobaIdOsoba")]
    [InverseProperty("Uzytkowniks")]
    public virtual Osoba OsobaIdOsobaNavigation { get; set; } = null!;

    [InverseProperty("UzytkownikIdUzytkownikaNavigation")]
    public virtual ICollection<PlanTreningowyUzytkownik> PlanTreningowyUzytkowniks { get; set; } = new List<PlanTreningowyUzytkownik>();

    [InverseProperty("UzytkownikIdUzytkownikaNavigation")]
    public virtual ICollection<UzytkownikBlog> UzytkownikBlogs { get; set; } = new List<UzytkownikBlog>();

    [InverseProperty("UzytkownikIdUzytkownikaNavigation")]
    public virtual ICollection<UzytkownikJadlospi> UzytkownikJadlospis { get; set; } = new List<UzytkownikJadlospi>();

    [InverseProperty("UzytkownikIdUzytkownikaNavigation")]
    public virtual ICollection<UzytkownikKursTrenera> UzytkownikKursTreneras { get; set; } = new List<UzytkownikKursTrenera>();
}
