using System.ComponentModel.DataAnnotations;

namespace GradskiMuzejSubotica_ZavrsniRad.Models
{
	public class RestauracijaBO
	{
		[Display(Name = "Šifra restauracije")]
		public int Id { get; set; }
		[Required]
		[Display(Name = "Šifra zahteva")]
		public int ZahtevId {  get; set; }
		[Required]
		[Display(Name = "Restaurator")]
		public int RestauratorId {  get; set; }
	}
}
