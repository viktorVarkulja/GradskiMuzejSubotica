using System.ComponentModel.DataAnnotations;

namespace GradskiMuzejSubotica_ZavrsniRad.Models.ViewModel
{
    public class PredmetViewModel
    {

        [Display(Name = "Inventarski broj")]
        public int Id { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Zbirka")]
        public int ZbirkaId { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Naziv")]
        public string? Name { get; set; }
        [DataType(DataType.Text)]
        [Display(Name = "Autor")]
        public string? Autor { get; set; }
        [DataType(DataType.Text)]
        [Display(Name = "Mesto")]
        public string? Mesto { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Vreme")]
        public DateTime? Vreme { get; set; }
        [DataType(DataType.Text)]
        [Display(Name = "Stil")]
        public string? Stil { get; set; }
        [DataType(DataType.Text)]
        [Display(Name = "Tehnika")]
        public string? Tehnika { get; set; }
        [DataType(DataType.Text)]
        [Display(Name = "Materijal")]
        public string? Materijal { get; set; }
        [DataType(DataType.Text)]
        [Display(Name = "Dimenzije")]
        public string? Dimenzije { get; set; }
        [DataType(DataType.Text)]
        [Display(Name = "BrDelova")]
        public string? BrDelova { get; set; }
        public int OdeljenjeId { get; set; }
        public int SifKorisnik {  get; set; }
    }
}
