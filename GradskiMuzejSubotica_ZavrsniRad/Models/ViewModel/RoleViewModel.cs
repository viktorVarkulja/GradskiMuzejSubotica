using System.ComponentModel.DataAnnotations;

namespace GradskiMuzejSubotica_ZavrsniRad.Models.Account
{
	public class RoleViewModel
	{
		[Required]
		[DataType(DataType.Text)]
		[Display(Name = "Name")]
		public string Name { get; set; }
	}
}
