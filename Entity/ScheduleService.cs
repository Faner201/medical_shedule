namespace Entity;
public class ScheduleService
{
    private IScheduleRepository _scheduleService;

    public ScheduleService(IScheduleRepository scheduleService)
    {
        _scheduleService = scheduleService;
    }

    public Result<Schedule> GetDoctorScheduleByDate(int doctorID, DateTime date)
    {
        var request = _scheduleService.GetDoctorScheduleByDate(doctorID, date);

        return request is null ? Result.Fail<Schedule>("The schedule for this doctor was not found ") : Result.Ok<Schedule>(request);
    }
    public Result<Schedule> AddScheduleDoctor(Schedule schedule)
    {
        var request = _scheduleService.AddScheduleDoctor(schedule);

        return request is null ? Result.Fail<Schedule>("I couldn't add the schedule") : Result.Ok<Schedule>(request);
    }
    public Result<Schedule> EditScheduleDoctor(Schedule actual, Schedule recent)
    {
        var request = _scheduleService.EditScheduleDoctor(actual, recent);

        return request is null ? Result.Fail<Schedule>("It was not possible to add changes to the schedule") : Result.Ok<Schedule>(request);
    }
}