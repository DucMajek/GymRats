using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GymRats.Server.Models;

[Table("Kurs_Trenera")]
public partial class KursTrenera
{
    [Key]
    [Column("id_kursu")]
    public int IdKursu { get; set; }

    [Column("nazwa")]
    [Unicode(false)]
    public string Nazwa { get; set; } = null!;

    [Column("czas_trwania")]
    [StringLength(5)]
    [Unicode(false)]
    public string CzasTrwania { get; set; } = null!;

    [Column("Trener_id_trenera")]
    public int TrenerIdTrenera { get; set; }

    [ForeignKey("TrenerIdTrenera")]
    [InverseProperty("KursTreneras")]
    public virtual Trener TrenerIdTreneraNavigation { get; set; } = null!;

    [InverseProperty("KursTreneraIdKursuNavigation")]
    public virtual ICollection<UzytkownikKursTrenera> UzytkownikKursTreneras { get; set; } = new List<UzytkownikKursTrenera>();
}
