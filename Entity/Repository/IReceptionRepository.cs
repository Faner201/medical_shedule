namespace Entity;
public interface IReceptionRepository : IRepository<Reception>
{
    Reception? SaveDoctorAppointment(Reception reception);
    Reception? SaveAnyFreeDoctorAppointment(Reception reception);
    List<(DateTime, DateTime)>? GetFreeAppointmentDateList(Specialization specialization, DateOnly date);
}