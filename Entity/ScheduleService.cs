namespace Entity;
public class ScheduleService
{
    private IScheduleRepository _scheduleService;
    private static SemaphoreSlim scheduleSemaphore = new SemaphoreSlim(1, 1);
    public ScheduleService(IScheduleRepository scheduleService)
    {
        _scheduleService = scheduleService;
    }

    async public Task<Result<Schedule>> GetDoctorScheduleByDate(int doctorID, DateTime date)
    {
        Schedule? request = null;
        try
        {
            await scheduleSemaphore.WaitAsync();

            request = await _scheduleService.GetDoctorScheduleByDate(doctorID, date);
        }
        finally
        {
            scheduleSemaphore.Release();
        }

        return request is null ? Result.Fail<Schedule>("The schedule for this doctor was not found ") : Result.Ok<Schedule>(request);
    }
    async public Task<Result<Schedule>> AddScheduleDoctor(Schedule schedule)
    {
        Schedule? request = null;
        try
        {
            await scheduleSemaphore.WaitAsync();

            request = await _scheduleService.AddScheduleDoctor(schedule);
        }
        finally
        {
            scheduleSemaphore.Release();
        }

        return request is null ? Result.Fail<Schedule>("I couldn't add the schedule") : Result.Ok<Schedule>(request);
    }
    async public Task<Result<Schedule>> EditScheduleDoctor(Schedule actual, Schedule recent)
    {
        Schedule? request = null;
        try
        {
            await scheduleSemaphore.WaitAsync();

            request = await _scheduleService.EditScheduleDoctor(actual, recent);
        }
        finally
        {
            scheduleSemaphore.Release();
        }

        return request is null ? Result.Fail<Schedule>("It was not possible to add changes to the schedule") : Result.Ok<Schedule>(request);
    }
}