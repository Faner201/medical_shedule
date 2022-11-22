namespace Entity;
public interface IScheduleRepository
{
   Schedule? GetDoctorScheduleByDate(int doctorID, DateTime date);
   Schedule? AddScheduleDoctor(Schedule schedule);
   Schedule? EditScheduleDoctor(Schedule actual, Schedule recent);
}