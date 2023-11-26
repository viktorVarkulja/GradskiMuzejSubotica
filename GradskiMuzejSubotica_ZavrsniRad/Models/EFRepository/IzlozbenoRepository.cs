using GradskiMuzejSubotica_ZavrsniRad.Models.Interfaces;

namespace GradskiMuzejSubotica_ZavrsniRad.Models.EFRepository
{
	public class IzlozbenoRepository : IIzlozbenoRepository
    {
		readonly GradskiMuzejSuboticaContext context = new GradskiMuzejSuboticaContext();
		readonly PredmetRepository predmetRepository = new PredmetRepository();
		readonly IzlozbaRepository izlozbaRepository = new IzlozbaRepository();
        /// <summary>
        /// Vraca listu IzlozbenoBO sa sve izlozbene 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IzlozbenoBO> GetAllIzlozbeno()
        {
            List<IzlozbenoBO> izlozbenoBOs = new List<IzlozbenoBO>();
            foreach(Izlozbeno izlozbeno in context.Izlozbenos.ToList())
            {
                IzlozbenoBO izlozbenoBO = new IzlozbenoBO();
                izlozbenoBO.IzlozbaID = izlozbeno.SifIzlozba;
                izlozbenoBO.PredmetID = izlozbeno.InvBr;

                izlozbenoBOs.Add(izlozbenoBO);
            }    

            return izlozbenoBOs;
        }
        /// <summary>
        /// Vraca predmete koji su izlozbene
        /// </summary>
        /// <returns></returns>
		public IEnumerable<PredmetBO> GetAllIzlozbenoPredmet()
		{
            List<PredmetBO> predmeti = new List<PredmetBO>();
            List<int> zavrsenIzlozbe = izlozbaRepository.GetZavrseneIzlozbeId();
            foreach(Izlozbeno izlozbeno in context.Izlozbenos.ToList())
            {
                if(!zavrsenIzlozbe.Contains(izlozbeno.SifIzlozba))
                {
                    PredmetBO predmet = predmetRepository.GetPredmetById(izlozbeno.InvBr);

                    predmeti.Add(predmet);
                }
                
            }

            return predmeti;
		}
        /// <summary>
        /// Vraca izlozbene sa sifrom izlozbe = id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
		public IEnumerable<IzlozbenoBO> GetIzlozbenosByIzloba(int id)
        {
            List<IzlozbenoBO> izlozbene = new List<IzlozbenoBO>();
            foreach (Izlozbeno izlozbeno in context.Izlozbenos.Where(x => x.SifIzlozba == id).ToList())
            {
                IzlozbenoBO izlozbenoBO = new IzlozbenoBO();
                izlozbenoBO.IzlozbaID = izlozbeno.SifIzlozba;
                izlozbenoBO.PredmetID = izlozbeno.InvBr;

                izlozbene.Add(izlozbenoBO);
            }

            return izlozbene;
        }
        /// <summary>
        /// Dodaje novo izlozbeno u bazu
        /// </summary>
        /// <param name="izlozbeno"></param>
        public void AddIzlozbeno(IzlozbenoBO izlozbeno)
        {
            Izlozbeno izl = new Izlozbeno();
            izl.SifIzlozba = izlozbeno.IzlozbaID;
            izl.InvBr = izlozbeno.PredmetID;
            context.Izlozbenos.Add(izl);
            context.SaveChanges();
        }
        /// <summary>
        /// Brise izlozbu iz baze
        /// </summary>
        /// <param name="sifIzl"></param>
        public void RemoveIzlozbeno(int sifIzl)
        {
            foreach(Izlozbeno i in context.Izlozbenos.Where(x=>x.SifIzlozba==sifIzl).ToList())
            {
                context.Izlozbenos.Remove(i);
            }
            
            context.SaveChanges();
        }
    }
}
