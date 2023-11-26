using System.ComponentModel.DataAnnotations;

namespace GradskiMuzejSubotica_ZavrsniRad.Models.ViewModel
{
    public class SearchPredmetViewModel
    {
        [Required]
		[Display(Name = "Izberite tip potrage")]
		public int SearchType { get; set; }
		[Display(Name = "Unesite podatak o predmetu")]
		public string? SearchName { get; set; }
        public DateTime? Date {  get; set; }
        public int? Korisnik { get; set; }
    }
}
