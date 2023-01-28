namespace Entity;
public class ReceptionService
{
    private IReceptionRepository _receptionService;
    private static Dictionary<int, SemaphoreSlim> semaphoreByDoctor = new Dictionary<int, SemaphoreSlim>();

    public ReceptionService(IReceptionRepository receptionSerive)
    {
        _receptionService = receptionSerive;
    }

    private SemaphoreSlim GetOrCreate(IDictionary<int, SemaphoreSlim> dict, int key)
    {
        if (!dict.TryGetValue(key, out SemaphoreSlim? val))
        {
            val = new SemaphoreSlim(1, 1);
            dict.Add(key, val);
        }

        return val;
    }

    async public Task<Result<Reception>> RecordCreation(Reception reception)
    {
        if (reception.SpecializationDoctor.Name == string.Empty) 
        {
            return Result.Fail<Reception>("There is no specialization");
        }

        if (await _receptionService.RecordExists(reception))
        {
            return Result.Fail<Reception>("There is already an appointment with this doctor for the selected date");
        }

        Reception? request = null;
        try
        {
            await GetOrCreate(semaphoreByDoctor, reception.IdDoctor).WaitAsync();

            request = await _receptionService.RecordCreation(reception);
        }
        finally
        {
            semaphoreByDoctor[reception.IdDoctor].Release();
            semaphoreByDoctor.Remove(reception.IdDoctor);
        }

        return request is not null ? Result.Ok<Reception>(request) : Result.Fail<Reception>("Failed to create appointment");
    }

    async public Task<Result<List<(DateTime, DateTime)>>> GetAllFreeDates(Specialization specialization, DateOnly date)
    {
        if (string.IsNullOrEmpty(specialization.Name))
            return Result.Fail<List<(DateTime, DateTime)>>("There is no specialization");

        var request = _receptionService.GetAllDates(specialization, date);

        var start = date.ToDateTime(new TimeOnly(0, 0, 0));
        var end = date.ToDateTime(new TimeOnly(23, 59, 59));

        var freeDates = new List<(DateTime, DateTime)>();

        var lastDate = (start, start);

        var busyDates = await request;

        if (busyDates.Count == 0)
        {
            return Result.Ok(new List<(DateTime, DateTime)>{(start, end)});
        }

        foreach(var currentDate in busyDates)
        {
            freeDates.Add((lastDate.Item2, currentDate.Item1));
            lastDate = currentDate;
        }

        if (busyDates.Last().Item2 != end)
        {
            freeDates.Add((busyDates.Last().Item2, end));
        }

        return Result.Ok<List<(DateTime, DateTime)>>(freeDates);
    }
}