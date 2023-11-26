using GradskiMuzejSubotica_ZavrsniRad.Models.Interfaces;
using System.Runtime.CompilerServices;

namespace GradskiMuzejSubotica_ZavrsniRad.Models.EFRepository
{
    public class StrucnjakRepository : IStrucnjakRepository
    {
		readonly GradskiMuzejSuboticaContext context = new();
		private static StrucnjakBO StrucnjakToStrucnjakBO(Strucnjak strucnjak, StrucnjakBO strucnjakBO)
        {
            strucnjakBO.Id = strucnjak.SifStrucnjak;
            strucnjakBO.KorisnikId = strucnjak.SifKorisnik;
            strucnjakBO.OdeljenjeId = strucnjak.SifOdelj;

            return strucnjakBO;
        }
        public StrucnjakBO GetStrucnjakByKorisnik(int korisnik)
        {
            Strucnjak strucnjak = context.Strucnjaks.Single(x => x.SifKorisnik == korisnik);
            StrucnjakBO strucnjakBO = new();

            strucnjakBO = StrucnjakToStrucnjakBO(strucnjak, strucnjakBO);
            return strucnjakBO;
        }
    }
}
