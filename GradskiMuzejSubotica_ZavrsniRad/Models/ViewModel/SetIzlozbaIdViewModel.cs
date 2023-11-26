using System.ComponentModel.DataAnnotations;

namespace GradskiMuzejSubotica_ZavrsniRad.Models.ViewModel
{
    public class SetIzlozbaIdViewModel
    {
        [Required]
        [Display(Name = "Šifra izložbe")]
        public int Id { get; set; }
    }
}
