namespace Entity;
public interface IReceptionRepository
{
    Task<Reception?> RecordCreation(Reception reception);
    Task<bool> RecordExists(Reception reception);
    Task<List<(DateTime, DateTime)>> GetAllDates(Specialization specialization, DateOnly date);
}