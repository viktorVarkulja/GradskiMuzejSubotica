namespace GradskiMuzejSubotica_ZavrsniRad.Models.Interfaces
{
    public interface IZbirkaRepository
    {
        IEnumerable<ZbirkaBO> GetAllZbirka();
        IEnumerable<ZbirkaBO> GetZbirkaByOdelj(int odelj);
        ZbirkaBO GetZbirkaById(int sifZbirka);

    }
}
