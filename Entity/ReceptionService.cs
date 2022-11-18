namespace Entity;
public class ReceptionService
{
    private IReceptionRepository _receptionService;

    public ReceptionService(IReceptionRepository receptionService)
    {
        _receptionService = receptionService;
    }

    public Result<Reception> SaveDoctorAppointment(Reception reception)
    {
        var request = _receptionService.SaveDoctorAppointment(reception);

        return request is null ? Result.Fail<Reception>("Sorry, this entry is already taken") : Result.Ok<Reception>(request);
    }
    public Result<Reception> SaveAnyFreeDoctorAppointment(Reception reception)
    {
        var request = _receptionService.SaveAnyFreeDoctorAppointment(reception);

        return request is null ? Result.Fail<Reception>("I'm sorry, I couldn't get an appointment") : Result.Ok<Reception>(request);
    }
    public Result<List<(DateTime, DateTime)>> GetFreeAppointmentDateList(Specialization specialization, DateOnly date)
    {
        if(string.IsNullOrEmpty(specialization.Name))
            return Result.Fail<List<(DateTime, DateTime)>>("This specialty was not found");
        
        var request = _receptionService.GetFreeAppointmentDateList(specialization, date);

        return request is null ? Result.Fail<List<(DateTime, DateTime)>>("There are no entries as of this date")
            : Result.Ok<List<(DateTime,DateTime)>>(request);
    }
}