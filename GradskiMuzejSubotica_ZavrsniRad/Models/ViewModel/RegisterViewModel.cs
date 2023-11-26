using System.ComponentModel.DataAnnotations;

namespace GradskiMuzejSubotica_ZavrsniRad.Models.Account
{
	public class RegisterViewModel
	{
		[Required]
		[DataType(DataType.Text)]
		[Display(Name = "Korisničko Ime")]
		public string UserName { get; set; }
		[Required]
		[DataType(DataType.Password)]
		[Display(Name = "Lozinka")]
		public string Password { get; set; }

		[DataType(DataType.Password)]
		[Display(Name = "Potvride lozinku")]
		[Compare("Password", ErrorMessage = "Lozinka i potvrda lozinke se ne podudaraju.")]
		public string ConfirmPassword { get; set; }

		[Required]
		[DataType(DataType.Text)]
		[Display(Name = "Ime i Prezime")]
		public string Name { get; set; }

		[Required]
		[Display(Name = "Rola")]
		public int Role { get; set; }
	}
}
