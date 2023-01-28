namespace Entity;
public class DoctorService
{
    private IDoctorRepository _doctorService;
    private static SemaphoreSlim doctorSemaphore = new SemaphoreSlim(1, 1);

    public DoctorService(IDoctorRepository doctor)
    {
        _doctorService = doctor;
    }

    async public Task<Result<Doctor>> CreateNewDoctor(Doctor doctor)
    {
        if(string.IsNullOrEmpty(doctor.Name))
            return Result.Fail<Doctor>("Nonexistent doctor name");
        if(doctor.Specialization is null)
            return Result.Fail<Doctor>("Doctor without a specialty");

        Doctor? request = null;

        try
        {
            await doctorSemaphore.WaitAsync();

            request = await _doctorService.CreateNewDoctor(doctor);
        }
        finally
        {
            doctorSemaphore.Release();
        }
        
        return request is null ? Result.Fail<Doctor>("Error when creating a doctor") : Result.Ok<Doctor>(doctor);
    }
    async public Task<Result<bool>> DeleteDoctor(int doctorID)
    {   
       bool request = false;
        try
        {
            await doctorSemaphore.WaitAsync();

            request = await _doctorService.DeleteDoctor(doctorID);
        }
        finally
        {
            doctorSemaphore.Release();
        }

        return request ? Result.Ok<bool>(request) : Result.Fail<bool>("Doctor not deleted");
    }
    async public Task<Result<List<Doctor>>> GetDoctorList()
    {
        List<Doctor> doctors = new List<Doctor>();
        try
        {
            await doctorSemaphore.WaitAsync();

            doctors = await _doctorService.GetDoctorList();
        }
        finally
        {
            doctorSemaphore.Release();
        }

        return doctors is null ? Result.Fail<List<Doctor>>("The list of doctors was empty") : Result.Ok(doctors);
    }
    async public Task<Result<Doctor>> GetDoctor(int doctorID)
    {
        Doctor? doctor = null;
        try
        {
            await doctorSemaphore.WaitAsync();

            doctor = await _doctorService.GetDoctor(doctorID);
        }
        finally
        {
            doctorSemaphore.Release();
        }
        
        return doctor is null ? Result.Fail<Doctor>("Doctor not found") : Result.Ok(doctor);
    }
    async public Task<Result<List<Doctor>>> GetDoctors(Specialization specialization)
    {
        if(string.IsNullOrEmpty(specialization.Name))
            return Result.Fail<List<Doctor>>("No existing specialization");

        List<Doctor> doctors = new List<Doctor>();
        try
        {
            await doctorSemaphore.WaitAsync();

            doctors = await _doctorService.GetDoctors(specialization);
        }
        finally
        {
            doctorSemaphore.Release();
        }

        return doctors is null ? Result.Fail<List<Doctor>>("Doctors not found") : Result.Ok(doctors);
    }
}