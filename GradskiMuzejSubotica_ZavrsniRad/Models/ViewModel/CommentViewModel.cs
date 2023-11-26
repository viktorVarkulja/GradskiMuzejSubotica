using System.ComponentModel.DataAnnotations;

namespace GradskiMuzejSubotica_ZavrsniRad.Models.ViewModel
{
    public class CommentViewModel
    {
        [Display(Name = "Šifra zahteva")]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Inventarski broj")]
        public int PredmetId { get; set; }
        [Required]
        [Display(Name = "Stručnjak")]
        public int SrucnjakId { get; set; }
        [Required]
        [Display(Name = "Status zahteva")]
        public int StatusId { get; set; }
        [Display(Name = "Komentar")]
        public string? Comment { get; set; }
        public int RestauratorId {  get; set; }
    }
}
