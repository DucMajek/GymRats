using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GymRats.Server.Models;

[Table("Uzytkownik_Blog")]
public partial class UzytkownikBlog
{
    [Key]
    [Column("id_uzytkownik_blog")]
    public int IdUzytkownikBlog { get; set; }

    [Column("Uzytkownik_id_uzytkownika")]
    public int UzytkownikIdUzytkownika { get; set; }

    [Column("Blog_id_blogu")]
    public int BlogIdBlogu { get; set; }

    [ForeignKey("BlogIdBlogu")]
    [InverseProperty("UzytkownikBlogs")]
    public virtual Blog BlogIdBloguNavigation { get; set; } = null!;

    [ForeignKey("UzytkownikIdUzytkownika")]
    [InverseProperty("UzytkownikBlogs")]
    public virtual Uzytkownik UzytkownikIdUzytkownikaNavigation { get; set; } = null!;
}
