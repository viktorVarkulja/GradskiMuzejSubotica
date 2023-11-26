namespace GradskiMuzejSubotica_ZavrsniRad.Models.Interfaces
{
    public interface IStrucnjakRepository
    {
        StrucnjakBO GetStrucnjakByKorisnik(int korisnik);
    }
}
