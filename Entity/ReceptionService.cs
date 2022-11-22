namespace Entity;
public class ReceptionService
{
    private IReceptionRepository _receptionService;

    public ReceptionService(IReceptionRepository receptionSerive)
    {
        _receptionService = receptionSerive;
    }

    public Result<Reception> RecordCreation(Reception reception)
    {
        if (reception.SpecializationDoctor.Name == string.Empty) 
        {
            return Result.Fail<Reception>("There is no specialization");
        }

        if (_receptionService.RecordExists(reception))
        {
            return Result.Fail<Reception>("There is already an appointment with this doctor for the selected date");
        }

        var request = _receptionService.RecordCreation(reception);

        return request is not null ? Result.Ok<Reception>(request) : Result.Fail<Reception>("Failed to create appointment");
    }

    public Result<List<(DateTime, DateTime)>> GetAllFreeDates(Specialization specialization, DateOnly date)
    {
        if (string.IsNullOrEmpty(specialization.Name))
            return Result.Fail<List<(DateTime, DateTime)>>("There is no specialization");

        var busyDates = _receptionService.GetAllDates(specialization, date);

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