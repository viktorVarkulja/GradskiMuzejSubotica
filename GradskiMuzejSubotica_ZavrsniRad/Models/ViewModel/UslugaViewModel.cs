using System.ComponentModel.DataAnnotations;

namespace GradskiMuzejSubotica_ZavrsniRad.Models.ViewModel
{
	public class UslugaViewModel
	{
		[Required]
		[Display(Name = "Usluge")]
		public int Usluga { get; set; }
		public int RadnikId { get; set; }
	}
}
