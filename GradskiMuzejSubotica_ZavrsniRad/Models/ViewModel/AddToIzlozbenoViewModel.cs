using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace GradskiMuzejSubotica_ZavrsniRad.Models.ViewModel
{
    public class AddToIzlozbenoViewModel
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
        public int BrSale { get; set; }
        [Required]
        public List<SelectListItem> Predmeti { get; set; }
    }
}
