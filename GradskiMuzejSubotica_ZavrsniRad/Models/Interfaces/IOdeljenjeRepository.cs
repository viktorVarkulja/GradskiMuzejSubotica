namespace GradskiMuzejSubotica_ZavrsniRad.Models.Interfaces
{
    public interface IOdeljenjeRepository
    {
        IEnumerable<OdeljenjeBO> GetAllOdelj();
        OdeljenjeBO GetOdeljenjeById(int idOdelj);
    }
}
