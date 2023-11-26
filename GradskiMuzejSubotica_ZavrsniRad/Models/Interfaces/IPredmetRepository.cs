namespace GradskiMuzejSubotica_ZavrsniRad.Models.Interfaces
{
    public interface IPredmetRepository
    {
        IEnumerable<PredmetBO> GetAllPredmet();
        IEnumerable<PredmetBO> GetPredmetsByOdeljenje(int sifOdelj);
        IEnumerable<PredmetBO> GetPredmetsByZbirka(int sifZbirka);
		IEnumerable<PredmetBO> GetPredmetsById(int invBr);
		PredmetBO GetPredmetById(int invBr);
        IEnumerable<PredmetBO> GetPredmetsByName(string naziv);
        void AddPredmet(PredmetBO predmetBO);
        void RemovePredmet(PredmetBO predmetBO);
        void EditPredmet(PredmetBO predmetBO);
    }
}
