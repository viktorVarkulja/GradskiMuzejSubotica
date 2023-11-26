using GradskiMuzejSubotica_ZavrsniRad.Models.Interfaces;

namespace GradskiMuzejSubotica_ZavrsniRad.Models.EFRepository
{
    public class OdeljenjeRepository : IOdeljenjeRepository
    {
		readonly GradskiMuzejSuboticaContext context = new GradskiMuzejSuboticaContext();
        public IEnumerable<OdeljenjeBO> GetAllOdelj()
        {
            List<OdeljenjeBO> odeljenja = new List<OdeljenjeBO>();
            foreach(Odeljenje o in context.Odeljenjes.ToList())
            {
                OdeljenjeBO odeljenje = new OdeljenjeBO();
                odeljenje.Id = o.SifOdelj;
                odeljenje.Name = o.Naziv;

                odeljenja.Add(odeljenje);
            }

            return odeljenja;
        }

        public OdeljenjeBO GetOdeljenjeById(int idOdelj)
        {
            Odeljenje o = context.Odeljenjes.Single(x => x.SifOdelj == idOdelj);
            OdeljenjeBO odeljenje = new OdeljenjeBO();
            odeljenje.Id = o.SifOdelj;
            odeljenje.Name = o.Naziv;

            return odeljenje;
        }
    }
}
