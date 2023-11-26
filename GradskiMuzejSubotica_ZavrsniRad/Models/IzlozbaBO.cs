using System.ComponentModel.DataAnnotations;

namespace GradskiMuzejSubotica_ZavrsniRad.Models
{
    public class IzlozbaBO
    {
        [Required]
        [Display(Name = "Šifra izložbe")]
        public int ID { get; set; }
        [Required]
        [Display(Name = "Naziv")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Datum početka")]
        public DateTime StartDate { get; set; }
        [Display(Name = "Datum kraja")]
        public DateTime? EndDate { get; set; }
        [Required]
        [Display(Name = "Broj sale")]
        public int BrSale {  get; set; }
    }
}
