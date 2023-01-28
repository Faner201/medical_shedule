using Microsoft.EntityFrameworkCore;
using Entity;

namespace Database;

public class ScheduleModelService : IScheduleRepository
{
    private ApplicationContext _db;

    public ScheduleModelService(ApplicationContext db)
    {
        _db = db;
    }

    async public Task<Schedule?> GetDoctorScheduleByDate(int doctorID, DateTime date)
    {
        var schedule = await _db.Schedule.FirstOrDefaultAsync(s => s.IdDoctor == doctorID && s.Start == date && s.End == date);

        if (schedule is null)
            return null;

        return new Schedule(
            schedule.IdDoctor,
            schedule.Start,
            schedule.End
        );
    }

    async public Task<Schedule?> AddScheduleDoctor(Schedule schedule)
    {
        var request = await _db.Schedule.FirstOrDefaultAsync(s => s.IdDoctor == schedule.IdDoctor &&
            s.Start == schedule.Start && s.End == schedule.End);

        if (request is not null)
            return null;

        await _db.Schedule.AddAsync(new ScheduleModel
            {
                IdDoctor = schedule.IdDoctor,
                Start = schedule.Start,
                End = schedule.End,
            }
        );
        await _db.SaveChangesAsync();

        return new Schedule(
            request.IdDoctor,
            request.Start,
            request.End
        );
    }

    async public Task<Schedule?> EditScheduleDoctor(Schedule actual, Schedule recent)
    {
        var request = await _db.Schedule.FirstOrDefaultAsync(s => s.IdDoctor == actual.IdDoctor && 
            s.Start == actual.Start && s.End == actual.End);
        if (request is not null)
        {
            request.IdDoctor = recent.IdDoctor;
            request.Start = recent.Start;
            request.End = recent.End;
            _db.Schedule.Update(request);
            await _db.SaveChangesAsync();
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