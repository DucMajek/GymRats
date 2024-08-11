using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GymRats.Server.Models;

[Table("Blog")]
public partial class Blog
{
    [Key]
    [Column("id_blogu")]
    public int IdBlogu { get; set; }

    [Column("tytul")]
    [StringLength(50)]
    [Unicode(false)]
    public string Tytul { get; set; } = null!;

    [Column("tresc")]
    [StringLength(50)]
    [Unicode(false)]
    public string Tresc { get; set; } = null!;

    [Column("data_publikacji", TypeName = "datetime")]
    public DateTime DataPublikacji { get; set; }

    [InverseProperty("BlogIdBloguNavigation")]
    public virtual ICollection<UzytkownikBlog> UzytkownikBlogs { get; set; } = new List<UzytkownikBlog>();
}
