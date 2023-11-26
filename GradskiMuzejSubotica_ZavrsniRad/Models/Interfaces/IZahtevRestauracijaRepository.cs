namespace GradskiMuzejSubotica_ZavrsniRad.Models.Interfaces
{
	public interface IZahtevRestauracijaRepository
	{
		IEnumerable<ZahtevRestauracijaBO> GetAllZahtev();
		IEnumerable<ZahtevRestauracijaBO> GetZahtevsByStatus(int status);
		void SendRestauracijaZahtev(int invBr, int sifStrucnjak);
		ZahtevRestauracijaBO? GetZahtevByPredmet(int invBr);
		ZahtevRestauracijaBO? GetZahtevById(int id);
		string GetZahtevStatusName(int id);
		void AcceptZahtev(int zahtevId, int sifRestaurator, string comment);
		void RejectZahtev(int zahtevId, string comment);
	}
}
