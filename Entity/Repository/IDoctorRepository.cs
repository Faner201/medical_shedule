namespace Entity;
public interface IDoctorRepository 
{
    Task<Doctor?> CreateNewDoctor(Doctor doctor);
    Task<bool> DeleteDoctor(int doctorID);
    Task<List<Doctor>?> GetDoctorList();
    Task<Doctor?> GetDoctor(int doctorID);
    Task<List<Doctor>?> GetDoctors(Specialization specialization);
}