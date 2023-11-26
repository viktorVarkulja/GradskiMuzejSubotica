namespace GradskiMuzejSubotica_ZavrsniRad.Models.Interfaces
{
    public interface IIzlozbaRepository
    {
        IEnumerable<IzlozbaBO> GetAllIzlozba();
        IEnumerable<IzlozbaBO> GetZavrseneIzlozbe();
        IEnumerable<IzlozbaBO> GetIzlozbasById(int id);
        IEnumerable<IzlozbaBO> GetIzlozbasByName(string name);
        IzlozbaBO GetIzlozbaById(int id);
        void AddIzlozba(IzlozbaBO izlozbaBO);
        void EditIZlozba(IzlozbaBO izlozbaBO);
        void RemoveIzlozba(IzlozbaBO izlozbaBO);
    }
}
