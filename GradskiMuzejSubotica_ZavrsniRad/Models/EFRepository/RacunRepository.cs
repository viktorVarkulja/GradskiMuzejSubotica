using GradskiMuzejSubotica_ZavrsniRad.Models.Interfaces;

namespace GradskiMuzejSubotica_ZavrsniRad.Models.EFRepository
{
	public class RacunRepository : IRacunRepository
	{
		readonly GradskiMuzejSuboticaContext context = new();
		/// <summary>
		/// Vraca Racun objekat sa podacima od RacunBO objekta
		/// </summary>
		/// <param name="racun"></param>
		/// <param name="racunBO"></param>
		/// <returns></returns>
		private static RacunBO RacunToRacunBO(Racun racun, RacunBO racunBO)
		{
			racunBO.Id = racun.SifRacun;
			racunBO.RadnikId = racun.SifKorisnik;
			racunBO.Time = racun.Vreme;
			racunBO.Date = racun.Datum;
			racunBO.UslugaId = racun.SifUsluga;
			racunBO.Amount = racun.Kolicina;
			racunBO.Vrednost = racun.Vrednost;

			return racunBO;
		}
		/// <summary>
		/// Vraca RacunBO objekat sa podacima od Racun objekta
		/// </summary>
		/// <param name="racunBO"></param>
		/// <param name="racun"></param>
		/// <returns></returns>
		private static Racun RacunBOToRacun(RacunBO racunBO, Racun racun)
		{
			racun.SifRacun = racunBO.Id;
			racun.SifKorisnik = racunBO.RadnikId;
			racun.SifUsluga = racunBO.UslugaId;
			racun.Datum = racunBO.Date;
			racun.Vreme = racunBO.Time;
			racun.Kolicina = racunBO.Amount;
			racun.Vrednost = racunBO.Vrednost;

			return racun;
		}
		/// <summary>
		/// Dodaje racun u bazu
		/// </summary>
		/// <param name="racunBO"></param>
		public void AddRacun(RacunBO racunBO)
		{
			Racun racun = new();
			racun = RacunBOToRacun(racunBO, racun);

			context.Racuns.Add(racun);
			context.SaveChanges();
		}
		/// <summary>
		/// Brise racun iz baze
		/// </summary>
		/// <param name="id"></param>
		public void DeleteRacun(int id)
		{
			Racun racun = context.Racuns.Single(x => x.SifRacun == id);

			context.Racuns.Remove(racun);
			context.SaveChanges();
		}
		/// <summary>
		/// Vraca sve racune u listu
		/// </summary>
		/// <returns></returns>
		public IEnumerable<RacunBO> GetAllRacun()
		{
			List<RacunBO> racuni = new();
			foreach(Racun r in context.Racuns.ToList())
			{
				RacunBO racun = new();
				racun = RacunToRacunBO(r, racun);

				racuni.Add(racun);
			}
			return racuni;
		}
		/// <summary>
		/// Vraca sve usluge u listu
		/// </summary>
		/// <returns></returns>
		public IEnumerable<UslugaBO> GetAllUsluga()
		{
			List<UslugaBO> usluge = new();
			foreach (Usluga u in context.Uslugas.ToList())
			{
				UslugaBO usluga = new();
				usluga.Id = u.SifUsluga;
				usluga.IzlozbaId = u.SifIzlozba;
				usluga.BrSale = u.BrSale;
				usluga.Name = u.Naziv;
				usluga.Price = u.Cena;

				usluge.Add(usluga);
			}
			return usluge;
		}
		/// <summary>
		/// Vraca racun sa sifrom = id
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public RacunBO GetRacunById(int id)
		{
			RacunBO racunBO = new();
			try
			{
				Racun racun = context.Racuns.Single(x => x.SifRacun == id);
				

				racunBO = RacunToRacunBO(racun, racunBO);
				
			}
			catch(Exception e)
			{
				racunBO = null;
			}
			return racunBO;
		}
		/// <summary>
		/// Vraca listu racuna sa datumom = date i vremenom = time
		/// </summary>
		/// <param name="date"></param>
		/// <param name="time"></param>
		/// <returns></returns>
		public IEnumerable<RacunBO> GetRacunsByDate(DateTime date)
		{
			List<RacunBO> racuni = new();
			foreach (Racun r in context.Racuns.Where(x => x.Datum == date).ToList()) 
			{
				RacunBO racun = new();
				racun = RacunToRacunBO(r, racun);

				racuni.Add(racun);
			}
			return racuni;
		}
		/// <summary>
		/// Vraca listu racuna sa sifrom = id
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public IEnumerable<RacunBO> GetRacunsById(int id)
		{
			List<RacunBO> racuni = new();
			foreach (Racun r in context.Racuns.Where(x => x.SifRacun == id).ToList()) 
			{
				RacunBO racun = new();
				racun = RacunToRacunBO(r, racun);

				racuni.Add(racun);
			}
			return racuni;
		}
		/// <summary>
		/// Vraca uslugu sa sifrom = id
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		/// <exception cref="NotImplementedException"></exception>
        public UslugaBO GetUslugaById(int id)
        {
			Usluga u = context.Uslugas.Single(x=>x.SifUsluga == id);
            
            UslugaBO usluga = new();
            usluga.Id = u.SifUsluga;
            usluga.IzlozbaId = u.SifIzlozba;
            usluga.BrSale = u.BrSale;
            usluga.Name = u.Naziv;
			usluga.Price = u.Cena;

            return usluga;
        }
    }
}
