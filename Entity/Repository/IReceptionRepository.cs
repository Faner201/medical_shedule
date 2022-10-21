public interface IReceptionRepository : IRepository<Reception>
{
    Reception? SaveDoctorAppointment(DateTime date, int doctorID);
    Reception? SaveDoctorAppointment(DateTime date);
    List<DateTime>? GetFreeAppointmentDateList(Specialization specialization);
}