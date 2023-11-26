using GradskiMuzejSubotica_ZavrsniRad.Models.Interfaces;

namespace GradskiMuzejSubotica_ZavrsniRad.Models.EFRepository
{
    public class KorisnikRepository : IKorisnikRepository
	{
		private readonly GradskiMuzejSuboticaContext context = new GradskiMuzejSuboticaContext();

		private int LastId()
		{
			int biggestId = 0;
			List<KorisnikBO> korisniks = (List<KorisnikBO>)GetAllKorisnik();
			foreach(KorisnikBO k in korisniks)
			{
				if (k.Id > biggestId) 
				{
					biggestId = k.Id;
				}
			}
			return biggestId;
		}

		public IEnumerable<KorisnikBO> GetAllKorisnik()
		{
			List<KorisnikBO> korisnici = new List<KorisnikBO>();
			foreach(Korisnik k in context.Korisniks)
			{
				KorisnikBO korisnikBO = new KorisnikBO();
				korisnikBO.UserName = k.KorisnickoIme;
				korisnikBO.Id = k.SifKorisnik;
				korisnikBO.Role = k.SifRola;
				korisnikBO.Name = k.ImePrezime;
				korisnikBO.Password = k.Lozinka;
				korisnici.Add(korisnikBO);
			}

			return korisnici;
		}
		public KorisnikBO GetKorisnikByUserName(string userName)
		{
			
			Korisnik k = context.Korisniks.Single(x=>x.KorisnickoIme == userName);
			
			KorisnikBO korisnikBO = new KorisnikBO();
			korisnikBO.UserName = k.KorisnickoIme;
			korisnikBO.Id = k.SifKorisnik;
			korisnikBO.Role = k.SifRola;
			korisnikBO.Name = k.ImePrezime;
			korisnikBO.Password = k.Lozinka;
				

			return korisnikBO;
		}
		public bool CheckUserName(string userName)
		{
			List<KorisnikBO> korisnici = (List<KorisnikBO>)GetAllKorisnik();
			bool userExists = korisnici.Exists(x => x.UserName.Equals(userName));

			return userExists;
		}
		public bool CheckPassword(string userName, string password)
		{
			List<KorisnikBO> korisnici = (List<KorisnikBO>)GetAllKorisnik();
			bool checkPassword = false;
			if (CheckUserName(userName))
			{
				KorisnikBO korisnik = korisnici.Find(k => k.UserName.Equals(userName));
				checkPassword = password.Equals(korisnik.Password);
			}

			return checkPassword;
		}
		
		public void AddKorisnik(KorisnikBO korisnikBO)
		{
			Korisnik k = new Korisnik();

			k.KorisnickoIme = korisnikBO.UserName;
			k.ImePrezime = korisnikBO.UserName;
			k.Lozinka = korisnikBO.Password;
			k.SifRola = korisnikBO.Role;
			k.SifKorisnik = LastId() + 1;

			context.Korisniks.Add(k);
			context.SaveChanges();
		}

		public bool ResgisterResult(KorisnikBO korisnikBO)
		{
			AddKorisnik(korisnikBO);
			bool result;
			if (GetKorisnikByUserName(korisnikBO.UserName) != null)
			{
				result = true;
			}
			else
			{
				result = false;
			}

			return result;
		}

        public string GetKorisnikName(int id)
        {
            Korisnik k = context.Korisniks.Single(x => x.SifKorisnik == id);

			string name = k.ImePrezime;


            return name;
        }

        public int GetKorisnikbyStrucnjak(int id)
        {
			Strucnjak strucnjak = context.Strucnjaks.Single(x => x.SifStrucnjak == id);
			return strucnjak.SifKorisnik;
        }
    }
}
