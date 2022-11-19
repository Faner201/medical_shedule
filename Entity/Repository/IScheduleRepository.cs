namespace Entity;
public interface IScheduleRepository : IRepository<Schedule>
{
   Schedule? GetDoctorScheduleByDate(int doctorID, DateTime date);
   Schedule? AddScheduleDoctor(Schedule schedule);
   Schedule? EditScheduleDoctor(Schedule schedule);
}