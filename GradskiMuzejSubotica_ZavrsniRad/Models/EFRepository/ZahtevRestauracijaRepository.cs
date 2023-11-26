using GradskiMuzejSubotica_ZavrsniRad.Models.Interfaces;

namespace GradskiMuzejSubotica_ZavrsniRad.Models.EFRepository
{
	public class ZahtevRestauracijaRepository : IZahtevRestauracijaRepository
	{
		readonly GradskiMuzejSuboticaContext context = new();
		/// <summary>
		/// Funkcija za prihvacenje zahteva za restauraciju
		/// </summary>
		/// <param name="zahtevId"></param>
		/// <param name="sifRestaurator"></param>
		/// <param name="comment"></param>
        public void AcceptZahtev(int zahtevId, int sifRestaurator, string comment)
        {
			try
			{
				ZahtevRestauracija zahtevRestauracija = context.ZahtevRestauracijas.Single(x => x.SifZahtev == zahtevId);
				zahtevRestauracija.SifStatus = 3;
				zahtevRestauracija.Komentar = comment;

				context.SaveChanges();

				Restauracija restauracija = new();
				restauracija.SifZahtev = zahtevId;
				restauracija.SifKorisnik = sifRestaurator;

				context.Restauracijas.Add(restauracija);
				context.SaveChanges();
			}   
			catch(Exception e)
			{

			}
        }

        /// <summary>
        /// Vraca sve zahteve
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public IEnumerable<ZahtevRestauracijaBO> GetAllZahtev()
		{
			List<ZahtevRestauracijaBO> zahtevRestauracije = new();
			foreach(ZahtevRestauracija zahtev in context.ZahtevRestauracijas.ToList())
			{
				ZahtevRestauracijaBO zahtevRestauracija = new();
				zahtevRestauracija.Id = zahtev.SifZahtev;
				zahtevRestauracija.PredmetId = zahtev.InvBr;
				zahtevRestauracija.SrucnjakId = zahtev.SifStrucnjak;
				zahtevRestauracija.StatusId = zahtev.SifStatus;
				zahtevRestauracija.Comment = zahtev.Komentar;

				zahtevRestauracije.Add(zahtevRestauracija);
			}

			return zahtevRestauracije;
		}
		/// <summary>
		/// Vraca zahtev sa sifrom = id
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		/// <exception cref="NotImplementedException"></exception>
        public ZahtevRestauracijaBO GetZahtevById(int id)
        {
            ZahtevRestauracijaBO? zahtev = new();
            try
            {
                ZahtevRestauracija zahtevRestauracija = context.ZahtevRestauracijas.Single(x => x.SifZahtev == id);
				zahtev.Id = zahtevRestauracija.SifZahtev;
                zahtev.PredmetId = zahtevRestauracija.InvBr;
                zahtev.SrucnjakId = zahtevRestauracija.SifStrucnjak;
                zahtev.Comment = zahtevRestauracija.Komentar;
                zahtev.StatusId = zahtevRestauracija.SifStatus;

                return zahtev;
            }
            catch (Exception ex)
            {
                zahtev = null;
                return zahtev;
            }
        }

		/// <summary>
		/// Vraca zahtev sa inventarskim brojem = invBr
		/// </summary>
		/// <param name="invBr"></param>
		/// <returns></returns>
		public ZahtevRestauracijaBO? GetZahtevByPredmet(int invBr)
		{
			ZahtevRestauracijaBO? zahtev = new();
			try
			{
				ZahtevRestauracija zahtevRestauracija = context.ZahtevRestauracijas.Where(x => x.InvBr == invBr).OrderBy(x=>x.SifZahtev).Last();
				zahtev.PredmetId = zahtevRestauracija.InvBr;
				zahtev.SrucnjakId = zahtevRestauracija.SifStrucnjak;
				zahtev.Comment = zahtevRestauracija.Komentar;
				zahtev.StatusId = zahtevRestauracija.SifStatus;

				return zahtev;
			}
			catch (Exception ex)
			{
				zahtev = null;
				return zahtev;
			}


		}
		/// <summary>
		/// Vraca zahteve sa satusom = status
		/// </summary>
		/// <param name="status"></param>
		/// <returns></returns>
		/// <exception cref="NotImplementedException"></exception>
		public IEnumerable<ZahtevRestauracijaBO> GetZahtevsByStatus(int status)
		{
            List<ZahtevRestauracijaBO> zahtevRestauracije = new();
			foreach (ZahtevRestauracija zahtev in context.ZahtevRestauracijas.Where(x => x.SifStatus == status).ToList()) 
            {
                ZahtevRestauracijaBO zahtevRestauracija = new();
                zahtevRestauracija.Id = zahtev.SifZahtev;
                zahtevRestauracija.PredmetId = zahtev.InvBr;
                zahtevRestauracija.SrucnjakId = zahtev.SifStrucnjak;
                zahtevRestauracija.StatusId = zahtev.SifStatus;
                zahtevRestauracija.Comment = zahtev.Komentar;

                zahtevRestauracije.Add(zahtevRestauracija);
            }

            return zahtevRestauracije;
        }

		/// <summary>
		/// Vraca opis statusa
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public string GetZahtevStatusName(int id)
		{
			StatusZahtev status = context.StatusZahtevs.Single(x => x.SifStatus == id);
			return status.OpisStatus;
		}
        /// <summary>
        /// Funkcija za odbijanje zahteva za restauraciju
        /// </summary>
        /// <param name="zahtevId"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void RejectZahtev(int zahtevId, string comment)
        {
			try
			{
                ZahtevRestauracija zahtevRestauracija = context.ZahtevRestauracijas.Single(x => x.SifZahtev == zahtevId);
                zahtevRestauracija.SifStatus = 2;
                zahtevRestauracija.Komentar = comment;

                context.SaveChanges();
            }
			catch(Exception e)
			{

			}
        }

        /// <summary>
        /// Upisuje novi zahtev za restauraciju u bazu
        /// </summary>
        /// <param name="predmetBO"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void SendRestauracijaZahtev(int invBr, int sifStrucnjak)
		{
			ZahtevRestauracija zahtev = new ZahtevRestauracija();
			zahtev.InvBr = invBr;
			zahtev.SifStrucnjak = sifStrucnjak;
			zahtev.Komentar = null;
			zahtev.SifStatus = 1;

			context.ZahtevRestauracijas.Add(zahtev);
			context.SaveChanges();
		}
		
		
	}
}
