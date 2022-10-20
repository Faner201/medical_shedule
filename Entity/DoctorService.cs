public class DoctorService
{
    private IDoctorRepository _db;

    public DoctorService(IDoctorRepository db)
    {
        _db = db;
    }

    public Result<Doctor> CreateNewDoctor(Doctor doctor)
    {
        if(string.IsNullOrEmpty(doctor.Name))
            return Result.Fail<Doctor>("Nonexistent doctor name");
        if(doctor.Specialization is null)
            return Result.Fail<Doctor>("Doctor without a specialty");

        var request = _db.CreateNewDoctor(doctor);
        
        return request is null ? Result.Fail<Doctor>("Error when creating a doctor") : Result.Ok<Doctor>(doctor);
    }
    public Result<bool> DeleteDoctor(int doctorID)
    {
        var doctor = _db.GetDoctor(doctorID);

        if(doctor is null)
            return Result.Fail<bool>("Doctor not found");
        
        bool request = _db.DeleteDoctor(doctorID);

        return request ? Result.Ok<bool>(request) : Result.Fail<bool>("Doctor not deleted");
    }
    public Result<List<Doctor>> GetDoctorList()
    {
        List<Doctor>? doctors = _db.GetDoctorList();

        return doctors is null ? Result.Fail<List<Doctor>>("The list of doctors was empty") : Result.Ok(doctors);
    }
    public Result<Doctor> GetDoctor(int doctorID)
    {
        Doctor? doctor = _db.GetDoctor(doctorID);
        
        return doctor is null ? Result.Fail<Doctor>("Doctor not found") : Result.Ok(doctor);
    }
    public Result<List<Doctor>> GetDoctors(Specialization specialization)
    {
        if(string.IsNullOrEmpty(specialization.Name))
            return Result.Fail<List<Doctor>>("No existing specialization");

        List<Doctor>? doctors = _db.GetDoctors(specialization);

        return doctors is null ? Result.Fail<List<Doctor>>("Doctors not found") : Result.Ok(doctors);
    }
}