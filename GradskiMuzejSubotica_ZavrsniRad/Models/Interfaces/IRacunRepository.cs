namespace GradskiMuzejSubotica_ZavrsniRad.Models.Interfaces
{
	public interface IRacunRepository
	{
		IEnumerable<RacunBO> GetAllRacun();
		IEnumerable<RacunBO> GetRacunsById(int id);
		IEnumerable<RacunBO> GetRacunsByDate(DateTime date);
		RacunBO GetRacunById(int id);
		IEnumerable<UslugaBO> GetAllUsluga();
		void AddRacun(RacunBO racunBO);
		void DeleteRacun(int id);
		UslugaBO GetUslugaById(int id);
	}
}
