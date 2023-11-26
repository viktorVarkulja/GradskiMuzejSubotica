using GradskiMuzejSubotica_ZavrsniRad.Models.Interfaces;

namespace GradskiMuzejSubotica_ZavrsniRad.Models.EFRepository
{
	public class RolaRepository : IRolaRepository
	{
		readonly GradskiMuzejSuboticaContext context = new GradskiMuzejSuboticaContext();
		public IEnumerable<RolaBO> GetAllRola()
		{
			List<RolaBO> rolaBOs = new List<RolaBO>();
			foreach (Rola r in context.Rolas)
			{
				RolaBO rolaBO = new()
				{
					Id = r.SifRola,
					Name = r.NazivRola
				};

				rolaBOs.Add(rolaBO);
			}
			return rolaBOs;
		}
		public void AddRola(RolaBO rolaBO)
		{
			Rola rola = new()
			{
				SifRola = rolaBO.Id,
				NazivRola = rolaBO.Name
			};

			context.Rolas.Add(rola);
			context.SaveChanges();
		}

		public bool CheckRole(int role)
		{
			List<RolaBO> roleBOs = (List<RolaBO>)GetAllRola();
			bool roleValid = roleBOs.Exists(x=>x.Id.Equals(role));

			return roleValid;
		}
	}
}
