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
        var request = _db.Schedule.FirstOrDefault(s => s.IdDoctor == schedule.IdDoctor);

        if (request is not null)
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
            request.IdDoctor,
            request.Start,
            request.End,
            request.Date
        );
    }

    public Schedule? EditScheduleDoctor(Schedule schedule)
    {
        var request = _db.Schedule.FirstOrDefault(s => s.IdDoctor == schedule.IdDoctor &&
        s.Date == schedule.Date && s.Start == schedule.Start && s.End == schedule.End);
        if (request is not null)
        {
            request.Start = schedule.Start;
            request.End = schedule.End;
            request.Date = schedule.Date;
            _db.Schedule.Update(schedule);
            _db.SaveChanges();
        } else {
            return null;
        }

        return new Schedule(
            request.IdDoctor,
            request.Start,
            request.End,
            request.Date
        );
    }
}