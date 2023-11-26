using System;
using System.Collections.Generic;

namespace GradskiMuzejSubotica_ZavrsniRad.Models;

public partial class ZahtevRestauracija
{
    public int SifZahtev { get; set; }

    public int InvBr { get; set; }

    public int SifStrucnjak { get; set; }

    public int SifStatus { get; set; }

    public string? Komentar { get; set; }

    public virtual Predmet InvBrNavigation { get; set; } = null!;

    public virtual ICollection<Restauracija> Restauracijas { get; set; } = new List<Restauracija>();

    public virtual StatusZahtev SifStatusNavigation { get; set; } = null!;

    public virtual Strucnjak SifStrucnjakNavigation { get; set; } = null!;
}
