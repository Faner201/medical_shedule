namespace Entity;
public class ScheduleService
{
    private IScheduleRepository _scheduleService;
    public ScheduleService(IScheduleRepository scheduleService)
    {
        _scheduleService = scheduleService;
    }

    async public Task<Result<Schedule>> GetDoctorScheduleByDate(int doctorID, DateTime date)
    {
        var request = await _scheduleService.GetDoctorScheduleByDate(doctorID, date);

        return request is null ? Result.Fail<Schedule>("The schedule for this doctor was not found ") : Result.Ok<Schedule>(request);
    }
    async public Task<Result<Schedule>> AddScheduleDoctor(Schedule schedule)
    {
        var request = await _scheduleService.AddScheduleDoctor(schedule);

        return request is null ? Result.Fail<Schedule>("I couldn't add the schedule") : Result.Ok<Schedule>(request);
    }
    async public Task<Result<Schedule>> EditScheduleDoctor(Schedule actual, Schedule recent)
    {
        var request = await _scheduleService.EditScheduleDoctor(actual, recent);

        return request is null ? Result.Fail<Schedule>("It was not possible to add changes to the schedule") : Result.Ok<Schedule>(request);
    }
}