using System;
using System.Collections.Generic;

namespace GradskiMuzejSubotica_ZavrsniRad.Models;

public partial class Zbirka
{
    public int SifZbirka { get; set; }

    public int SifOdelj { get; set; }

    public string Naziv { get; set; } = null!;

    public virtual ICollection<Predmet> Predmets { get; set; } = new List<Predmet>();

    public virtual Odeljenje SifOdeljNavigation { get; set; } = null!;
}
