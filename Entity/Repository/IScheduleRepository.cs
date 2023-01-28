namespace Entity;
public interface IScheduleRepository
{
   Task<Schedule?> GetDoctorScheduleByDate(int doctorID, DateTime date);
   Task<Schedule?> AddScheduleDoctor(Schedule schedule);
   Task<Schedule?> EditScheduleDoctor(Schedule actual, Schedule recent);
}