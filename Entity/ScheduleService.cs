public class ScheduleService
{
    private IScheduleRepository _db;

    public ScheduleService(IScheduleRepository db)
    {
        _db = db;
    }

    public Result<Schedule> GetDoctorScheduleByDate(int doctorID, DateTime date)
    {
        var request = _db.GetDoctorScheduleByDate(doctorID, date);

        return request is null ? Result.Fail<Schedule>("The schedule for this doctor was not found ") : Result.Ok<Schedule>(request);
    }
    public Result<Schedule> AddScheduleDoctor(Schedule schedule)
    {
        var request = _db.AddScheduleDoctor(schedule);

        return request is null ? Result.Fail<Schedule>("I couldn't add the schedule") : Result.Ok<Schedule>(request);
    }
    public Result<Schedule> EditScheduleDoctor(Schedule schedule)
    {
        var request = _db.EditScheduleDoctor(schedule);

        return request is null ? Result.Fail<Schedule>("It was not possible to add changes to the schedule") : Result.Ok<Schedule>(request);
    }
}