using GradskiMuzejSubotica_ZavrsniRad.Models.Interfaces;

namespace GradskiMuzejSubotica_ZavrsniRad.Models.EFRepository
{
    public class ZbirkaRepository : IZbirkaRepository
    {
        readonly GradskiMuzejSuboticaContext context = new GradskiMuzejSuboticaContext();
        public IEnumerable<ZbirkaBO> GetAllZbirka()
        {
            List<ZbirkaBO> zbirke = new List<ZbirkaBO>();
            foreach(Zbirka z in context.Zbirkas.ToList())
            {
				ZbirkaBO zbirka = new()
				{
					Id = z.SifZbirka,
					Name = z.Naziv,
					OdeljenjeId = z.SifOdelj
				};
				zbirke.Add(zbirka);
            }
            return zbirke;
        }

        public ZbirkaBO GetZbirkaById(int sifZbirka)
        {
			ZbirkaBO zbirka = new();
			Zbirka z = context.Zbirkas.Single(x => x.SifZbirka == sifZbirka);
            zbirka.Id = z.SifZbirka;
            zbirka.Name = z.Naziv;
            zbirka.OdeljenjeId = z.SifOdelj;

            return zbirka;
        }

        public IEnumerable<ZbirkaBO> GetZbirkaByOdelj(int odelj)
        {
            List<ZbirkaBO> zbirke = new List<ZbirkaBO>();
            foreach (Zbirka z in context.Zbirkas.Where(x => x.SifOdelj == odelj).ToList()) 
            {
				ZbirkaBO zbirka = new()
				{
					Id = z.SifZbirka,
					Name = z.Naziv,
					OdeljenjeId = z.SifOdelj
				};
				zbirke.Add(zbirka);
            }
            return zbirke;
        }
    }
}
