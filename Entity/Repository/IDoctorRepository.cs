public interface IDoctorRepository : IRepository<Doctor>
{
    Doctor? CreateNewDoctor(Doctor doctor);
    bool DeleteDoctor(int doctorID);
    List<Doctor>? GetDoctorList();
    Doctor? GetDoctor(int doctorID);
    List<Doctor>? GetDoctors(Specialization specialization);
}