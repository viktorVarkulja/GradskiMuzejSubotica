namespace GradskiMuzejSubotica_ZavrsniRad.Models.Interfaces
{
    public interface IIzlozbenoRepository
    {
        IEnumerable<IzlozbenoBO> GetAllIzlozbeno();
        IEnumerable<PredmetBO> GetAllIzlozbenoPredmet();
        IEnumerable<IzlozbenoBO> GetIzlozbenosByIzloba(int id);
        void AddIzlozbeno(IzlozbenoBO izlozbeno);
        void RemoveIzlozbeno(int sifIzl);
    }
}
