using GradskiMuzejSubotica_ZavrsniRad.Models.Interfaces;

namespace GradskiMuzejSubotica_ZavrsniRad.Models.EFRepository
{
    public class PredmetRepository : IPredmetRepository
    {
		readonly GradskiMuzejSuboticaContext context = new GradskiMuzejSuboticaContext();
        /// <summary>
        /// Vraca PredmetBO objekat sa podacima iz Predmet objekta
        /// </summary>
        /// <param name="predmetBO"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        private static PredmetBO PredmetToPredmetBO(PredmetBO predmetBO, Predmet p)
        {
            predmetBO.Id = p.InvBr;
            predmetBO.Name = p.Naziv;
            predmetBO.ZbirkaId = p.SifZbirka;
            predmetBO.Autor = p.Autor;
            predmetBO.Materijal = p.Materijal;
            predmetBO.Vreme = (DateTime?)p.Vreme;
            predmetBO.Mesto = p.Mesto;
            predmetBO.Dimenzije = p.Dimenzije;
            predmetBO.Stil = p.Stil;
            predmetBO.Tehnika = p.Tehnika;
            predmetBO.BrDelova = p.BrDelova;

            return predmetBO;
        }
		/// <summary>
		/// Vraca Predmet objekat sa podacima iz PredmetBO objekta
		/// </summary>
		/// <param name="predmet"></param>
		/// <param name="predmetBO"></param>
		/// <returns></returns>
		private static Predmet PredmetBOToPredmet(Predmet predmet, PredmetBO predmetBO)
        {
            predmet.InvBr = predmetBO.Id;
            predmet.Naziv = predmetBO.Name;
            predmet.SifZbirka = predmetBO.ZbirkaId;
            predmet.Autor = predmetBO.Autor;
            predmet.Materijal = predmetBO.Materijal;
            predmet.Vreme = predmetBO.Vreme;
            predmet.Mesto = predmetBO.Mesto;
            predmet.Dimenzije = predmetBO.Dimenzije;
            predmet.Stil = predmetBO.Stil;
            predmet.Tehnika = predmetBO.Tehnika;
            predmet.BrDelova = predmetBO.BrDelova;

            return predmet;
        }
        /// <summary>
        /// Dodaje predmet u bazu
        /// </summary>
        /// <param name="predmetBO"></param>
        public void AddPredmet(PredmetBO predmetBO)
        {
            Predmet predmet = new Predmet();
            predmet = PredmetBOToPredmet(predmet, predmetBO);

            context.Predmets.Add(predmet);
            context.SaveChanges();
        }
        /// <summary>
        /// Menja podatke o predmetu
        /// </summary>
        /// <param name="predmetBO"></param>
        public void EditPredmet(PredmetBO predmetBO)
        {
            Predmet predmet = context.Predmets.Single(p => p.InvBr == predmetBO.Id);
            predmet = PredmetBOToPredmet(predmet, predmetBO);

            context.SaveChanges();
        }
        /// <summary>
        /// Vraca sve predmete u listu
        /// </summary>
        /// <returns></returns>
        public IEnumerable<PredmetBO> GetAllPredmet()
        {
            List<PredmetBO> predmeti = new List<PredmetBO>();
            foreach(Predmet p in context.Predmets.ToList())
            {
                PredmetBO predmetBO = new PredmetBO();
                predmetBO = PredmetToPredmetBO(predmetBO, p);

                predmeti.Add(predmetBO);
            }

			if (predmeti.Count == 0)
			{
				predmeti = null;
			}

			return predmeti;
        }
        /// <summary>
        /// Vraca predmet sa inventarskim brojem = invBr
        /// </summary>
        /// <param name="invBr"></param>
        /// <returns></returns>
        public PredmetBO GetPredmetById(int invBr)
        {
			PredmetBO predmetBO = new PredmetBO();
			try
            {
				Predmet p = context.Predmets.Single(x => x.InvBr == invBr);
				
				predmetBO = PredmetToPredmetBO(predmetBO, p);
            }
            catch(Exception ex)
            {
                predmetBO = null;
            }
            

            return predmetBO;
        }
        /// <summary>
        /// Vraca listu predmeta sa inventarskim brojem = invBr
        /// </summary>
        /// <param name="invBr"></param>
        /// <returns></returns>
		public IEnumerable<PredmetBO> GetPredmetsById(int invBr)
		{
			List<PredmetBO> predmeti = new List<PredmetBO>();
			try
			{
				foreach (Predmet p in context.Predmets.Where(x => x.InvBr == invBr))
				{
					PredmetBO predmetBO = new PredmetBO();
					predmetBO = PredmetToPredmetBO(predmetBO, p);

                    predmeti.Add(predmetBO);
				}
			}
			catch (Exception ex)
			{
				predmeti = null;
			}

			if (predmeti.Count == 0)
			{
				predmeti = null;
			}

			return predmeti;
		}
        /// <summary>
        /// Vraca listu predmeta sa nazivom = naziv
        /// </summary>
        /// <param name="naziv"></param>
        /// <returns></returns>
		public IEnumerable<PredmetBO> GetPredmetsByName(string naziv)
        {
            List<PredmetBO> predmeti = new List<PredmetBO>();
            try
            {
				foreach (Predmet p in context.Predmets.Where(x => x.Naziv.Contains(naziv)).ToList())
				{
					PredmetBO predmetBO = new PredmetBO();
					predmetBO = PredmetToPredmetBO(predmetBO, p);

                    predmeti.Add(predmetBO);
				}
			}
			catch (Exception ex)
			{
				predmeti = null;
			}

			if (predmeti.Count == 0)
			{
				predmeti = null;
			}

			return predmeti;
        }
        /// <summary>
        /// Vraca listu predmeta iz odeljenja sa sifrom = sifOdelj
        /// </summary>
        /// <param name="sifOdelj"></param>
        /// <returns></returns>
        public IEnumerable<PredmetBO> GetPredmetsByOdeljenje(int sifOdelj)
        {
            List<PredmetBO> predmeti = new List<PredmetBO>();
            try
            {
                foreach (Zbirka z in context.Zbirkas.Where(x => x.SifOdelj == sifOdelj).ToList()) 
                {
                    foreach (Predmet p in context.Predmets.Where(b => b.SifZbirka == z.SifZbirka).ToList())
                    {
                        PredmetBO predmetBO = new PredmetBO();
                        predmetBO = PredmetToPredmetBO(predmetBO, p);
                        predmeti.Add(predmetBO);
                    }
                }
            }
			catch (Exception ex)
			{
				predmeti = null;
			}


			return predmeti;
        }
        /// <summary>
        /// Vraca listu predmeta sa sifrom zbirke = sifZbirka
        /// </summary>
        /// <param name="sifZbirka"></param>
        /// <returns></returns>
        public IEnumerable<PredmetBO> GetPredmetsByZbirka(int sifZbirka)
        {
            List<PredmetBO>? predmeti = new List<PredmetBO>();
            foreach(Predmet p in context.Predmets.Where(x=>x.SifZbirka == sifZbirka).ToList())
            {
                PredmetBO predmetBO = new PredmetBO();
                predmetBO = PredmetToPredmetBO(predmetBO, p); 

                predmeti.Add(predmetBO);
            }

			if (predmeti.Count == 0)
			{
				predmeti = null;
			}

			return predmeti;
        }
        /// <summary>
        /// Brise predmet iz baze
        /// </summary>
        /// <param name="predmetBO"></param>
        public void RemovePredmet(PredmetBO predmetBO)
        {
            Predmet p = context.Predmets.Single(x => x.InvBr == predmetBO.Id);

            context.Predmets.Remove(p);
            context.SaveChanges();
        }
    }
}
