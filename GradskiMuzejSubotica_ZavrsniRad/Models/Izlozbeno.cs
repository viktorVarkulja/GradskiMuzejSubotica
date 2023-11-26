using System;
using System.Collections.Generic;

namespace GradskiMuzejSubotica_ZavrsniRad.Models;

public partial class Izlozbeno
{
    public int SifIzlozbeno { get; set; }

    public int InvBr { get; set; }

    public int SifIzlozba { get; set; }

    public virtual Predmet InvBrNavigation { get; set; } = null!;

    public virtual Izlozba SifIzlozbaNavigation { get; set; } = null!;
}
