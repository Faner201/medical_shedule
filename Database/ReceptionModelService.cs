namespace Database;

using Entity;

public class ReceptionModelService : IReceptionRepository
{
    private ApplicationContext _db;

    public ReceptionModelService(ApplicationContext db)
    {
        _db = db;
    }

    public Reception? SaveDoctorAppointment(Reception reception)
    {
        var receptions = _db.Reception.FirstOrDefault(r => r.IdDoctor == reception.IdDoctor &&
        r.IdUser == reception.IdUser && r.Begin == reception.Begin && r.End == reception.End);

        if (receptions is null)
            return null;
        
        _db.Reception.Add(
            new ReceptionModel{
                Begin = receptions.Begin,
                End = receptions.End,
                IdUser = reception.IdUser,
                IdDoctor = reception.IdDoctor
            }
        );

        _db.SaveChanges();

        return new Reception(
            receptions.Begin,
            receptions.End,
            receptions.IdUser,
            receptions.IdDoctor
        );
    }

    public Reception? SaveAnyFreeDoctorAppointment(DateTime date)
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