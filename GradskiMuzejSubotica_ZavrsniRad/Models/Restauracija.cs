using System;
using System.Collections.Generic;

namespace GradskiMuzejSubotica_ZavrsniRad.Models;

public partial class Restauracija
{
    public int SifRestauracija { get; set; }

    public int SifZahtev { get; set; }

    public int SifKorisnik { get; set; }

    public virtual Korisnik SifKorisnikNavigation { get; set; } = null!;

    public virtual ZahtevRestauracija SifZahtevNavigation { get; set; } = null!;
}
