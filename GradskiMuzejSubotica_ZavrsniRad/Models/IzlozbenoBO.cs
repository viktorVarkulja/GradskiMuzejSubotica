using System.ComponentModel.DataAnnotations;

namespace GradskiMuzejSubotica_ZavrsniRad.Models
{
    public class IzlozbenoBO
    {
        [Required]
        [Display(Name = "Šifra izložbe")]
        public int IzlozbaID { get; set; }
        //[Required]
        [Display(Name = "Inventarski broj")]
        public int PredmetID { get; set; }
    }
}
