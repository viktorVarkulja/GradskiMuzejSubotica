using GradskiMuzejSubotica_ZavrsniRad.Models.Interfaces;

namespace GradskiMuzejSubotica_ZavrsniRad.Models.EFRepository
{
    public class IzlozbaRepository : IIzlozbaRepository
    {
		readonly GradskiMuzejSuboticaContext context = new GradskiMuzejSuboticaContext();
		/// <summary>
		/// Vraca Izlozba objekat sa podacima iz IzlozbaBO objekta
		/// </summary>
		/// <param name="i"></param>
		/// <param name="izlozbaBO"></param>
		/// <returns></returns>
		private static IzlozbaBO IzlozbaToIzlozbaBO(Izlozba i, IzlozbaBO izlozbaBO)
        {
            izlozbaBO.ID = i.SifIzlozba;
            izlozbaBO.Name = i.Naziv;
            izlozbaBO.StartDate = i.DatumPocetak;
            izlozbaBO.EndDate = i.DatumKraja;
            izlozbaBO.BrSale = i.BrSale;

            return izlozbaBO;
        }
        /// <summary>
        /// Vraca IzlozbaBO objekat sa podacima iz Izlozba objekta
        /// </summary>
        /// <param name="izlozbaBO"></param>
        /// <param name="izlozba"></param>
        /// <returns></returns>
        private static Izlozba IzlozbaBOToIzlozba(IzlozbaBO izlozbaBO, Izlozba izlozba)
        {
            izlozba.SifIzlozba = izlozbaBO.ID;
            izlozba.Naziv = izlozbaBO.Name;
            izlozba.DatumPocetak = izlozbaBO.StartDate;
            izlozba.DatumKraja = izlozbaBO.EndDate;
            izlozba.BrSale = izlozbaBO.BrSale;

            return izlozba;
        }
        /// <summary>
        /// Dodaja izlozbu u bazu
        /// </summary>
        /// <param name="izlozbaBO"></param>
        public void AddIzlozba(IzlozbaBO izlozbaBO)
        {
            Izlozba izlozba = new Izlozba();
            izlozba = IzlozbaBOToIzlozba(izlozbaBO, izlozba);

            Usluga usluga = new();
            usluga.SifIzlozba = izlozbaBO.ID;
            usluga.Naziv = "Ulaznica - " + izlozbaBO.Name;
            usluga.BrSale = izlozbaBO.BrSale;
            usluga.Cena = 150;

            context.Izlozbas.Add(izlozba);
            context.Uslugas.Add(usluga);
            context.SaveChanges();
        }
        /// <summary>
        /// Menja podatke izlozbe u bazi
        /// </summary>
        /// <param name="izlozbaBO"></param>
        public void EditIZlozba(IzlozbaBO izlozbaBO)
        {
            Izlozba izlozba = context.Izlozbas.Single(x => x.SifIzlozba == izlozbaBO.ID);
            Usluga usluga = context.Uslugas.Single(x => x.SifIzlozba == izlozbaBO.ID);
			usluga.SifIzlozba = izlozbaBO.ID;
			usluga.Naziv = "Ulaznica - " + izlozbaBO.Name;
			usluga.BrSale = izlozbaBO.BrSale;
			usluga.Cena = 150;
			izlozba = IzlozbaBOToIzlozba(izlozbaBO, izlozba);

            context.SaveChanges();
        }
        /// <summary>
        /// Vraca sve izlozbe
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IzlozbaBO> GetAllIzlozba()
        {
			List<IzlozbaBO>? izlozbe;
			try
			{
                izlozbe = new List<IzlozbaBO>();
                foreach (Izlozba i in context.Izlozbas.ToList())
                {
                    IzlozbaBO izlozba = new IzlozbaBO();
                    izlozba = IzlozbaToIzlozbaBO(i, izlozba);

                    izlozbe.Add(izlozba);
                }
            }
			catch (Exception)
			{
                izlozbe = null;
            }

            return izlozbe;
        }
        /// <summary>
        /// Vraca izlozbu sa sifrom = id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IzlozbaBO GetIzlozbaById(int id)
        {
            IzlozbaBO izlozba = new IzlozbaBO();
            try
            {
                Izlozba i = context.Izlozbas.Single(x => x.SifIzlozba == id);
                izlozba = IzlozbaToIzlozbaBO(i, izlozba);
            }
            catch(Exception e)
            {
                izlozba = null;
            }

            return izlozba;
            
        }
        /// <summary>
        /// Vraca listu izlozba sa sifrom id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IEnumerable<IzlozbaBO> GetIzlozbasById(int id)
        {
            List<IzlozbaBO>? izlozbe = null;
            try
            {
                izlozbe = new List<IzlozbaBO>();
                foreach (Izlozba i in context.Izlozbas.Where(x=>x.SifIzlozba == id).ToList())
                {
                    IzlozbaBO izlozba = new IzlozbaBO();
                    izlozba = IzlozbaToIzlozbaBO(i, izlozba);

                    izlozbe.Add(izlozba);
                }
            }
            catch (Exception e)
            {
                izlozbe = null;
            }

            return izlozbe;
        }
        /// <summary>
        /// Vraca listu izlozbe sa nazivom = naziv
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IEnumerable<IzlozbaBO> GetIzlozbasByName(string name)
        {
            List<IzlozbaBO>? izlozbe = null;
            try
            {
                izlozbe = new List<IzlozbaBO>();
                foreach (Izlozba i in context.Izlozbas.Where(x=>x.Naziv.Contains(name)).ToList())
                {
                    IzlozbaBO izlozba = new IzlozbaBO();
                    izlozba = IzlozbaToIzlozbaBO(i, izlozba);

                    izlozbe.Add(izlozba);
                }
            }
            catch (Exception e)
            {
                izlozbe = null;
            }

            return izlozbe;
        }
       /// <summary>
       /// Vraca listu izlozbe koji su se zavrsili
       /// </summary>
       /// <returns></returns>
        public IEnumerable<IzlozbaBO> GetZavrseneIzlozbe()
        {
            List<IzlozbaBO>? izlozbe = null;
            try
            {
                izlozbe = new List<IzlozbaBO>();
                foreach (Izlozba i in context.Izlozbas.Where(x => x.DatumKraja != null).ToList()) 
                {
                    IzlozbaBO izlozba = new IzlozbaBO();
                    izlozba = IzlozbaToIzlozbaBO(i, izlozba);

                    izlozbe.Add(izlozba);
                }
            }
            catch (Exception e)
            {
                izlozbe = null;
            }

            return izlozbe;
        }
        /// <summary>
        /// Vraca listu sifru izlozba koji su se zavrzili
        /// </summary>
        /// <returns></returns>
        public List<int> GetZavrseneIzlozbeId()
        {
            List<int>? izlozbe = null;
            try
            {
                izlozbe = new List<int>();
                foreach (Izlozba i in context.Izlozbas.Where(x => x.DatumKraja != null).ToList())
                {
                    izlozbe.Add(i.SifIzlozba);
                }
            }
            catch (Exception e)
            {
                izlozbe = null;
            }

            return izlozbe;
        }
        /// <summary>
        /// Brise izlizbu iz baze
        /// </summary>
        /// <param name="izlozbaBO"></param>
        public void RemoveIzlozba(IzlozbaBO izlozbaBO)
        {
            Izlozba izlozba = context.Izlozbas.Single(x => x.SifIzlozba == izlozbaBO.ID);
            Usluga usluga = context.Uslugas.Single(x => x.SifIzlozba == izlozbaBO.ID);

            context.Uslugas.Remove(usluga);
            context.Izlozbas.Remove(izlozba);
            context.SaveChanges();
        }
    }
}
