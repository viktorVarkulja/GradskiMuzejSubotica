namespace GradskiMuzejSubotica_ZavrsniRad.Models
{
    public class UslugaBO
    {
        public int Id {  get; set; }
        public string Name { get; set; }
        public int? IzlozbaId { get; set; }
        public int? BrSale {  get; set; }
        public float Price {  get; set; }

    }
}
