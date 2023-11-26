using System.ComponentModel.DataAnnotations;

namespace GradskiMuzejSubotica_ZavrsniRad.Models
{
    public class RacunBO
    {
        [Required]
        [Display(Name = "Šifra računa")]
        public int Id {  get; set; }
        [Required]
        [Display(Name = "Usluga")]
        public int UslugaId { get; set; }
        [Required]
        [Display(Name = "Količina")]
        public int Amount { get; set; }
        [Required]
        [Display(Name = "Datum")]
        public DateTime Date { get; set; }
        [Required]
        [Display(Name = "Vreme")]
        public TimeSpan Time { get; set; }
        [Required]
        [Display(Name = "Vrednost")]
        public float Vrednost { get; set; }
        [Required]
        [Display(Name = "Radnik")]
        public int RadnikId { get; set; }
    }
}
