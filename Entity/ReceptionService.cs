namespace Entity;
public class ReceptionService
{
    private IReceptionRepository _receptionService;
    private static SemaphoreSlim receptionSemaphore = new SemaphoreSlim(1, 1);

    public ReceptionService(IReceptionRepository receptionSerive)
    {
        _receptionService = receptionSerive;
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
            await receptionSemaphore.WaitAsync();

            request = await _receptionService.RecordCreation(reception);
        }
        finally
        {
            receptionSemaphore.Release();
        }

        return request is not null ? Result.Ok<Reception>(request) : Result.Fail<Reception>("Failed to create appointment");
    }

    async public Task<Result<List<(DateTime, DateTime)>>> GetAllFreeDates(Specialization specialization, DateOnly date)
    {
        if (string.IsNullOrEmpty(specialization.Name))
            return Result.Fail<List<(DateTime, DateTime)>>("There is no specialization");

        var freeDates = new List<(DateTime, DateTime)>();
        try {
            await receptionSemaphore.WaitAsync();

            var result = _receptionService.GetAllDates(specialization, date);

            var start = date.ToDateTime(new TimeOnly(0, 0, 0));
            var end = date.ToDateTime(new TimeOnly(23, 59, 59));

            var lastDate = (start, start);

            var busyDates = await result;

            if (busyDates.Count == 0) {
                receptionSemaphore.Release();
                return Result.Ok(new List<(DateTime, DateTime)>{(start, end)});
            }
            
            foreach(var currentDate in busyDates)
            {
                freeDates.Add((lastDate.Item2, currentDate.Item1));
                lastDate = currentDate;
            }

            if (busyDates.Last().Item2 != end) {
                freeDates.Add((busyDates.Last().Item2, end));
            }
        }
        finally {
            receptionSemaphore.Release();
        }
        return Result.Ok<List<(DateTime, DateTime)>>(freeDates);
    }
}