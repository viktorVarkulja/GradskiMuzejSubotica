using System;
using System.Collections.Generic;

namespace GradskiMuzejSubotica_ZavrsniRad.Models;

public partial class Usluga
{
    public int SifUsluga { get; set; }

    public string Naziv { get; set; } = null!;

    public int? SifIzlozba { get; set; }

    public int? BrSale { get; set; }

    public float Cena { get; set; }

    public virtual ICollection<Racun> Racuns { get; set; } = new List<Racun>();

    public virtual Izlozba? SifIzlozbaNavigation { get; set; }
}
