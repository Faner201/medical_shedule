namespace Entity;
public class ScheduleService
{
    private IScheduleRepository _scheduleService;

    public ScheduleService(IScheduleRepository scheduleSerive)
    {
        _scheduleService = scheduleSerive;
    }

    public Result<Schedule> RecordCreation(Schedule schedule)
    {
        if (schedule.SpecializationDoctor.Name == string.Empty) 
        {
            return Result.Fail<Schedule>("There is no specialization");
        }

        if (_scheduleService.RecordExists(schedule))
        {
            return Result.Fail<Schedule>("There is already an appointment with this doctor for the selected date");
        }

        var request = _scheduleService.RecordCreation(schedule);

        return request is not null ? Result.Ok<Schedule>(request) : Result.Fail<Schedule>("Failed to create appointment");
    }

    public Result<List<(DateTime, DateTime)>> GetAllFreeDates(Specialization specialization, DateOnly date)
    {
        if (string.IsNullOrEmpty(specialization.Name))
            return Result.Fail<List<(DateTime, DateTime)>>("There is no specialization");

        var busyDates = _scheduleService.GetAllDates(specialization, date);

        DateTime startTime = date.ToDateTime(new TimeOnly(0, 0, 0));
        DateTime endTime = date.ToDateTime(new TimeOnly(23, 59, 59));

        var allFreeDates = new List<(DateTime, DateTime)>();
        var lastDate = (startTime, startTime);

        if (busyDates.Count == 0)
            return Result.Ok(new List<(DateTime, DateTime)>{(startTime, endTime)});
        
        foreach (var currentDate in busyDates)
        {
            allFreeDates.Add((lastDate.Item2, currentDate.Item1));
            lastDate = currentDate;
        }

        if (busyDates.Last().Item2 != endTime)
            allFreeDates.Add((busyDates.Last().Item2, endTime));

        return Result.Ok<List<(DateTime, DateTime)>>(allFreeDates);
            
    }

}