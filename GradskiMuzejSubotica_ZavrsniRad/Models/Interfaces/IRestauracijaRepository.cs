namespace GradskiMuzejSubotica_ZavrsniRad.Models.Interfaces
{
    public interface IRestauracijaRepository
    {
        IEnumerable<RestauracijaBO> GetAllRestauracija();
        RestauracijaBO GetRestauracijaByZahtev(int zahtevId);
        void FinishRestauracija(int id, string comment);
    }
}
