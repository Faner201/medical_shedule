namespace Database;

using Entity;

public class ReceptionModelService : IReceptionRepository
{
    private ApplicationContext _db;

    public ReceptionModelService(ApplicationContext db)
    {
        _db = db;
    }

    public Reception SaveDoctorAppointment(DateTime date, int doctorID)
    {
        //вот тут я не понимаю, как можно вытащить id user, да и как
        //дату разделить на начало и конец
    }

    public Reception SaveDoctorAppointment(DateTime date)
    {
        //та же самая проблема, что и вверху
    }

    public List<DateTime> GetFreeAppointmentDateList(Specialization specialization)
    {
        var requestSpec = _db.Doctor.Where(u => u.Specialization == specialization);
        var requestRec = _db.Reception.Where(u => u.IdDoctor == requestSpec.IdDoctor);

        return new List<DateTime> {requestRec.Start, requestRec.End};
    }
}