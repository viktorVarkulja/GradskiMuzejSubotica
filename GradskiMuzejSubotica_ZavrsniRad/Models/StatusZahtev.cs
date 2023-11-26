using System;
using System.Collections.Generic;

namespace GradskiMuzejSubotica_ZavrsniRad.Models;

public partial class StatusZahtev
{
    public int SifStatus { get; set; }

    public string OpisStatus { get; set; } = null!;

    public virtual ICollection<ZahtevRestauracija> ZahtevRestauracijas { get; set; } = new List<ZahtevRestauracija>();
}
