using System;
using System.Collections.Generic;

namespace GradskiMuzejSubotica_ZavrsniRad.Models;

public partial class Predmet
{
    public int InvBr { get; set; }

    public int SifZbirka { get; set; }

    public string Naziv { get; set; } = null!;

    public string? Autor { get; set; }

    public string? Mesto { get; set; }

    public DateTime? Vreme { get; set; }

    public string? Stil { get; set; }

    public string? Tehnika { get; set; }

    public string? Materijal { get; set; }

    public string? Dimenzije { get; set; }

    public string? BrDelova { get; set; }

    public virtual ICollection<Izlozbeno> Izlozbenos { get; set; } = new List<Izlozbeno>();

    public virtual Zbirka SifZbirkaNavigation { get; set; } = null!;

    public virtual ICollection<ZahtevRestauracija> ZahtevRestauracijas { get; set; } = new List<ZahtevRestauracija>();
}
