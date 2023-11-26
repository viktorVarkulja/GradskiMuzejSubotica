using System;
using System.Collections.Generic;

namespace GradskiMuzejSubotica_ZavrsniRad.Models;

public partial class Izlozba
{
    public int SifIzlozba { get; set; }

    public string Naziv { get; set; } = null!;

    public DateTime DatumPocetak { get; set; }

    public DateTime? DatumKraja { get; set; }

    public int BrSale { get; set; }

    public virtual ICollection<Izlozbeno> Izlozbenos { get; set; } = new List<Izlozbeno>();

    public virtual ICollection<Usluga> Uslugas { get; set; } = new List<Usluga>();
}
