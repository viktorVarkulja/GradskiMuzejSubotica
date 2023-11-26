using System;
using System.Collections.Generic;

namespace GradskiMuzejSubotica_ZavrsniRad.Models;

public partial class Strucnjak
{
    public int SifStrucnjak { get; set; }

    public int SifKorisnik { get; set; }

    public int SifOdelj { get; set; }

    public virtual Korisnik SifKorisnikNavigation { get; set; } = null!;

    public virtual Odeljenje SifOdeljNavigation { get; set; } = null!;

    public virtual ICollection<ZahtevRestauracija> ZahtevRestauracijas { get; set; } = new List<ZahtevRestauracija>();
}
