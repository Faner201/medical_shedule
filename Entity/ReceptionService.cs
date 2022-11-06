namespace Entity;
public class ReceptionService
{
    private IReceptionRepository _receptionService;

    public ReceptionService(IReceptionRepository receptionService)
    {
        _receptionService = receptionService;
    }

    public Result<Reception> SaveDoctorAppointment(DateTime date, int doctorID)
    {
        var request = _receptionService.SaveDoctorAppointment(date, doctorID);

        return request is null ? Result.Fail<Reception>("Sorry, this entry is already taken") : Result.Ok<Reception>(request);
    }
    public Result<Reception> SaveDoctorAppointment(DateTime date)
    {
        var request = _receptionService.SaveDoctorAppointment(date);

        return request is null ? Result.Fail<Reception>("I'm sorry, I couldn't get an appointment") : Result.Ok<Reception>(request);
    }
    public Result<List<DateTime>> GetFreeAppointmentDateList(Specialization specialization)
    {
        if(string.IsNullOrEmpty(specialization.Name))
            return Result.Fail<List<DateTime>>("This specialty was not found");
        
        var request = _receptionService.GetFreeAppointmentDateList(specialization);

        return request is null ? Result.Fail<List<DateTime>>("There are no entries as of this date") : Result.Ok<List<DateTime>>(request);
    }
}