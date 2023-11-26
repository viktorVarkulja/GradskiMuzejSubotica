using System.ComponentModel.DataAnnotations;

namespace GradskiMuzejSubotica_ZavrsniRad.Models
{
	public class StrucnjakBO
	{
		[Display(Name = "Šifra stručnjaka")]
		public int Id { get; set; }
		[Required]
		[Display(Name = "Korisnik")]
		public int KorisnikId { get; set; }
		[Required]
		[Display(Name = "Odeljenje")]
		public int OdeljenjeId { get; set; }
	}
}
