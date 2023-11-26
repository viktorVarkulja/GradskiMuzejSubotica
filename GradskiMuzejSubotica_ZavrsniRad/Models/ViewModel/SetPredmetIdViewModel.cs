using System.ComponentModel.DataAnnotations;

namespace GradskiMuzejSubotica_ZavrsniRad.Models.ViewModel
{
    public class SetPredmetIdViewModel
    {
        [Required]
        [Display(Name = "Inventarski broj")]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Odeljenje")]
        public int Odelj { get; set; }
    }
}
