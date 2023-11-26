namespace GradskiMuzejSubotica_ZavrsniRad.Models.Interfaces
{
	public interface IRolaRepository
	{
		IEnumerable<RolaBO> GetAllRola();
		void AddRola(RolaBO rolaBO);
		bool CheckRole(int role);
	}
}
