using Entity;

namespace Database;

public class DoctorModelService : IDoctorRepository
{
    private ApplicationContext _db;

    public DoctorModelService(ApplicationContext db)
    {
        _db = db;
    }

    public Doctor? CreateNewDoctor(Doctor doctor)
    {
        var request = _db.Doctor.FirstOrDefault(d => d.Name == doctor.Name && d.Specialization.Id == doctor.Specialization.Id &&
            d.Id == doctor.Id && d.Specialization.Name == doctor.Specialization.Name);

        if (request is null)
            return null;

        _db.Doctor.Add(new DoctorModel 
            {
                Name = doctor.Name,
                Specialization =  new SpecializationModel(){
                    Name = doctor.Specialization.Name
                }
            }
        );

        _db.SaveChanges();

        return new Doctor(
            request.Id,
            request.Name,
            new Specialization(request.Specialization.Id, request.Specialization.Name)
        );
    }

    public bool DeleteDoctor(int doctorID)
    {
        var request = _db.Doctor.FirstOrDefault(u => u.Id == doctorID);

        if (request != null)
        {
            _db.Doctor.Remove(request);
            _db.SaveChanges();
            return true;
        }
        return false;
    }

    public List<Doctor>? GetDoctorList()
    {
       var request = _db.Doctor
            .Select(d => new Doctor(d.Id, d.Name, new Specialization(d.Specialization.Id, d.Specialization.Name)))
            .ToList();

        if (request is null)
            return null;
        
        return request;
    }

    public Doctor? GetDoctor(int doctorID)
    {
        var doctor = _db.Doctor.FirstOrDefault(d => d.Id == doctorID);

        if (doctor is null)
            return null;

        return new Doctor(
            doctor.Id,
            doctor.Name,
            new Specialization(doctor.Specialization.Id, doctor.Specialization.Name)
        );
    }

    public List<Doctor> GetDoctors(Specialization specialization)
    {
        return  _db.Doctor
            .Where(d => d.Specialization.Name == specialization.Name && d.Specialization.Id == specialization.Id)
            .Select(d => new Doctor(d.Id, d.Name, new Specialization(d.Specialization.Id, d.Specialization.Name)))
            .ToList();
    }
}