using System;
using System.Collections.Generic;

namespace GradskiMuzejSubotica_ZavrsniRad.Models;

public partial class Racun
{
    public int SifRacun { get; set; }

    public int SifUsluga { get; set; }

    public int Kolicina { get; set; }

    public DateTime Datum { get; set; }

    public TimeSpan Vreme { get; set; }

    public float Vrednost { get; set; }

    public int SifKorisnik { get; set; }

    public virtual Korisnik SifKorisnikNavigation { get; set; } = null!;

    public virtual Usluga SifUslugaNavigation { get; set; } = null!;
}
