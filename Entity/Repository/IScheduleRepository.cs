namespace Entity;
public interface IScheduleRepository : IRepository<Schedule>
{
   Schedule? RecordCreation(Schedule schedule);
   bool RecordExists(Schedule schedule);

   List<(DateTime, DateTime)> GetAllDates(Specialization specialization, DateOnly date);
}