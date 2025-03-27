using System;
using System.Collections.Generic;

namespace GymRats.Data.Entities;

public partial class Blog
{
    public int IdBlogu { get; set; }

    public string Tytul { get; set; } = null!;

    public string Tresc { get; set; } = null!;

    public DateTime DataPublikacji { get; set; }

    public virtual ICollection<UzytkownikBlog> UzytkownikBlogs { get; set; } = new List<UzytkownikBlog>();
}
