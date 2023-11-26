using System;
using System.Collections.Generic;

namespace GradskiMuzejSubotica_ZavrsniRad.Models;

public partial class Odeljenje
{
    public int SifOdelj { get; set; }

    public string Naziv { get; set; } = null!;

    public virtual ICollection<Strucnjak> Strucnjaks { get; set; } = new List<Strucnjak>();

    public virtual ICollection<Zbirka> Zbirkas { get; set; } = new List<Zbirka>();
}
