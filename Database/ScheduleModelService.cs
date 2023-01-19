using Entity;

namespace Database;

public class ScheduleModelService : IScheduleRepository
{
    private ApplicationContext _db;

    public ScheduleModelService(ApplicationContext db)
    {
        _db = db;
    }

    public Schedule? GetDoctorScheduleByDate(int doctorID, DateTime date)
    {
        var schedule = _db.Schedule.FirstOrDefault(s => s.IdDoctor == doctorID && s.Start == date && s.End == date);

        if (schedule is null)
            return null;

        return new Schedule(
            schedule.IdDoctor,
            schedule.Start,
            schedule.End
        );
    }

    public Schedule? AddScheduleDoctor(Schedule schedule)
    {
        var request = _db.Schedule.FirstOrDefault(s => s.IdDoctor == schedule.IdDoctor &&
            s.Start == schedule.Start && s.End == schedule.End);

        if (request is not null)
            return null;

        _db.Schedule.Add(new ScheduleModel
            {
                IdDoctor = schedule.IdDoctor,
                Start = schedule.Start,
                End = schedule.End,
            }
        );
        _db.SaveChanges();

        return new Schedule(
            request.IdDoctor,
            request.Start,
            request.End
        );
    }

    public Schedule? EditScheduleDoctor(Schedule actual, Schedule recent)
    {
        var request = _db.Schedule.FirstOrDefault(s => s.IdDoctor == actual.IdDoctor && 
            s.Start == actual.Start && s.End == actual.End);
        if (request is not null)
        {
            request.IdDoctor = recent.IdDoctor;
            request.Start = recent.Start;
            request.End = recent.End;
            _db.Schedule.Update(request);
            _db.SaveChanges();
        } else {
            return null;
        }

        return new Schedule(
            request.IdDoctor,
            request.Start,
            request.End
        );
    }
}