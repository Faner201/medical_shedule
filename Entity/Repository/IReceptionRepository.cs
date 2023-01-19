namespace Entity;
public interface IReceptionRepository
{
    Reception? RecordCreation(Reception reception);
    bool RecordExists(Reception reception);
    List<(DateTime, DateTime)> GetAllDates(Specialization specialization, DateOnly date);
}