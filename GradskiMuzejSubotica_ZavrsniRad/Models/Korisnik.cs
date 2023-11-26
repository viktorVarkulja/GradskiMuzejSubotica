using System;
using System.Collections.Generic;

namespace GradskiMuzejSubotica_ZavrsniRad.Models;

public partial class Korisnik
{
    public int SifKorisnik { get; set; }

    public string KorisnickoIme { get; set; } = null!;

    public string Lozinka { get; set; } = null!;

    public string ImePrezime { get; set; } = null!;

    public int SifRola { get; set; }

    public virtual ICollection<Racun> Racuns { get; set; } = new List<Racun>();

    public virtual ICollection<Restauracija> Restauracijas { get; set; } = new List<Restauracija>();

    public virtual Rola SifRolaNavigation { get; set; } = null!;

    public virtual ICollection<Strucnjak> Strucnjaks { get; set; } = new List<Strucnjak>();
}
