namespace Database;

using Entity;

public class ScheduleModelService : IScheduleRepository
{
    private ApplicationContext _db;

    public ScheduleModelService(ApplicationContext db)
    {
        _db = db;
    }

    public Schedule? GetDoctorScheduleByDate(int doctorID, DateOnly date)
    {
        var schedule = _db.Schedule.Where(s => s.IdDoctor == doctorID && s.Date == date);

        if (schedule is null)
            return null;

        return new Schedule(
            schedule.IdDoctor,
            schedule.Start,
            schedule.End,
            schedule.Date
        );
    }

    public Schedule? AddScheduleDoctor(Schedule schedule)
    {
        var schedules = _db.Schedule.FirstOrDefault(s => s.IdDoctor == schedule.IdDoctor);

        if (schedules is not null)
            return null;

        _db.Schedule.Add(new ScheduleModel
            {
                IdDoctor = schedule.IdDoctor,
                Start = schedule.Start,
                End = schedule.End
            }
        );
        _db.SaveChanges();

        return new Schedule(
            schedules.IdDoctor,
            schedules.Start,
            schedules.End,
            schedules.Date
        );
    }

    public Schedule? EditScheduleDoctor(Schedule schedule)
    {
        var schedules = _db.Schedule.FirstOrDefault(s => s.IdDoctor == schedule.IdDoctor &&
        s.Date == schedule.Date && s.Start == schedule.Start && s.End == schedule.End);
        if (schedule is not null)
        {
            schedules.Start = schedule.Start;
            schedules.End = schedule.End;
            schedules.Date = schedule.Date;
            _db.Schedule.Update(schedule);
            _db.SaveChanges();
        } else {
            return null;
        }

        return new Schedule(
            schedules.IdDoctor,
            schedules.Start,
            schedules.End,
            schedules.Date
        );
    }
}