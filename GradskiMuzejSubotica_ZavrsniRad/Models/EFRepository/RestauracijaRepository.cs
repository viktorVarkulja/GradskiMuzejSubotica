using GradskiMuzejSubotica_ZavrsniRad.Models.Interfaces;

namespace GradskiMuzejSubotica_ZavrsniRad.Models.EFRepository
{
    public class RestauracijaRepository : IRestauracijaRepository
    {
		readonly GradskiMuzejSuboticaContext context = new();
        /// <summary>
        /// Funkcija za zavrsetak restauracije
        /// </summary>
        /// <param name="id"></param>
        /// <param name="comment"></param>
        public void FinishRestauracija(int id, string comment)
        {
            try
            {
                Restauracija restauracija = context.Restauracijas.Single(x => x.SifRestauracija == id);
                ZahtevRestauracija zahtev = context.ZahtevRestauracijas.Single(x => x.SifZahtev == restauracija.SifZahtev);
                zahtev.Komentar = comment;
                zahtev.SifStatus = 4;

                context.SaveChanges();
            }
            catch(Exception e)
            {

            }

        }
        /// <summary>
        /// Vraca sve restauracije
        /// </summary>
        /// <returns></returns>
        public IEnumerable<RestauracijaBO> GetAllRestauracija()
        {
            List<RestauracijaBO> restauracije = new List<RestauracijaBO>();
            foreach(Restauracija r in context.Restauracijas.ToList())
            {
                RestauracijaBO restauracijaBO = new();
                restauracijaBO.Id = r.SifRestauracija;
                restauracijaBO.RestauratorId = r.SifKorisnik;
                restauracijaBO.ZahtevId = r.SifZahtev;

                restauracije.Add(restauracijaBO);
            }
            return restauracije;
        }
        /// <summary>
        /// Vraca restauraciju sa sifrom zahteva = zahtevId
        /// </summary>
        /// <param name="zahtevId"></param>
        /// <returns></returns>
        public RestauracijaBO GetRestauracijaByZahtev(int zahtevId)
        {
            Restauracija r = context.Restauracijas.Single(x => x.SifZahtev == zahtevId);
            
            RestauracijaBO restauracijaBO = new();
            restauracijaBO.Id = r.SifRestauracija;
            restauracijaBO.RestauratorId = r.SifKorisnik;
            restauracijaBO.ZahtevId = r.SifZahtev;

            return restauracijaBO;
        }
    }
}
