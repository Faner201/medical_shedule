namespace Entity;
public class DoctorService
{
    private IDoctorRepository _doctorService;

    public DoctorService(IDoctorRepository doctor)
    {
        _doctorService = doctor;
    }

    public Result<Doctor> CreateNewDoctor(Doctor doctor)
    {
        if(string.IsNullOrEmpty(doctor.Name))
            return Result.Fail<Doctor>("Nonexistent doctor name");
        if(doctor.Specialization is null)
            return Result.Fail<Doctor>("Doctor without a specialty");

        var request = _doctorService.CreateNewDoctor(doctor);
        
        return request is null ? Result.Fail<Doctor>("Error when creating a doctor") : Result.Ok<Doctor>(doctor);
    }
    public Result<bool> DeleteDoctor(int doctorID)
    {
        var doctor = _doctorService.GetDoctor(doctorID);

        if(doctor is null)
            return Result.Fail<bool>("Doctor not found");
        
        bool request = _doctorService.DeleteDoctor(doctorID);

        return request ? Result.Ok<bool>(request) : Result.Fail<bool>("Doctor not deleted");
    }
    public Result<List<Doctor>> GetDoctorList()
    {
        List<Doctor>? doctors = _doctorService.GetDoctorList();

        return doctors is null ? Result.Fail<List<Doctor>>("The list of doctors was empty") : Result.Ok(doctors);
    }
    public Result<Doctor> GetDoctor(int doctorID)
    {
        Doctor? doctor = _doctorService.GetDoctor(doctorID);
        
        return doctor is null ? Result.Fail<Doctor>("Doctor not found") : Result.Ok(doctor);
    }
    public Result<List<Doctor>> GetDoctors(Specialization specialization)
    {
        if(string.IsNullOrEmpty(specialization.Name))
            return Result.Fail<List<Doctor>>("No existing specialization");

        List<Doctor>? doctors = _doctorService.GetDoctors(specialization);

        return doctors is null ? Result.Fail<List<Doctor>>("Doctors not found") : Result.Ok(doctors);
    }
}