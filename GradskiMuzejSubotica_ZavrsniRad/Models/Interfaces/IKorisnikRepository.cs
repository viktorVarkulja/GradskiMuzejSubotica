namespace GradskiMuzejSubotica_ZavrsniRad.Models.Interfaces
{
	public interface IKorisnikRepository
	{
		IEnumerable<KorisnikBO> GetAllKorisnik();
		KorisnikBO GetKorisnikByUserName(string userName);
		string GetKorisnikName(int id);
		bool CheckUserName(string userName);
		bool CheckPassword(string userName, string password);
		int GetKorisnikbyStrucnjak(int id);
		void AddKorisnik(KorisnikBO korisnikBO);
		bool ResgisterResult(KorisnikBO korisnikBO);
	}
}
