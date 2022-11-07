namespace Database;

using Entity;

public class ScheduleModelService : IScheduleRepository
{
    private ApplicationContext _db;

    public ScheduleModelService(ApplicationContext db)
    {
        _db = db;
    }

    public Schedule GetDoctorScheduleByDate(int doctorID, DateTime date)
    {
        var request = _db.Schedule.Where(u => u.IdDoctor == doctorID && u.Start = date);

        return new Schedule{
            IdDoctor = request.IdDoctor,
            Start = request.Start,
            End = request.End
        };
    }

    public Schedule AddScheduleDoctor(Schedule schedule)
    {
        _db.Schedule.Add(new ScheduleModel{
            IdDoctor = schedule.IdDoctor,
            Start = schedule.Start,
            End = schedule.End
        });
        _db.SaveChanges();
    }

    public Schedule EditScheduleDoctor(Schedule schedule)
    {
        var request = _db.Schedule.FirstOrDefault(u => u.IdDoctor == schedule.IdDoctor);
        if(request)
        {
            request.Start = schedule.Start;
            request.End = schedule.End;
            _db.Schedule.Update(request);
        }
    }
}