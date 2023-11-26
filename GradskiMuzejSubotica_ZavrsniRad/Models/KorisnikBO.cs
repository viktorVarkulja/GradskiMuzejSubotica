using System.ComponentModel.DataAnnotations;

namespace GradskiMuzejSubotica_ZavrsniRad.Models
{
	public class KorisnikBO
	{
		[Required]
		public int Id {  get; set; }
		[Required]
		public string UserName { get; set; }
		[Required]
		public string Password { get; set; }
		[Required]
		public string Name { get; set; }
		[Required]
		public int Role {  get; set; }
	}
}
