using System.ComponentModel.DataAnnotations;

namespace GradskiMuzejSubotica_ZavrsniRad.Models.Account
{
	public class LoginViewModel
	{
		[Required]
		[DataType(DataType.Text)]
		[Display(Name = "Korisničko Ime")]
		public string UserName { get; set; }
		[Required]
		[DataType(DataType.Password)]
        [Display(Name = "Lozinka")]
        public string Password { get; set; }
	}
}
