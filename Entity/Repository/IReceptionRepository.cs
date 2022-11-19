namespace Entity;
public interface IReceptionRepository : IRepository<Reception>
{
    Reception? RecordCreation(Reception reception);
    bool RecordExists(Reception reception);
    List<(DateTime, DateTime)> GetAllDates(Specialization specialization, DateOnly date);
}