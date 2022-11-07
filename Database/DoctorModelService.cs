namespace Database;

using Entity;

public class DoctorModelService : IDoctorRepository
{
    private ApplicationContext _db;

    public DoctorModelService(ApplicationContext db)
    {
        _db = db;
    }

    public Doctor CreateNewDoctor(Doctor doctor)
    {
        _db.Doctor.Add(new DoctorModel {
            Name = doctor.Name,
            Specialization = doctor.Specialization
        });
        _db.SaveChanges();
    }

    public bool DeleteDoctor(int DoctorID)
    {
        var request = _db.Doctor.FirstOrDefault(u => u.Id == DoctorID);
        if (request != null)
        {
            _db.Doctor.Remove(request);
            _db.SaveChanges();
            return true;
        }
        return false;
    }

    public List<Doctor> GetDoctorList()
    {
        var request = from r in DoctorModel select r;

        return new List<Doctor>(new Doctor {
            Id = request.Id, Name = request.Name, 
            Specialization = request.Specialization
        });
    }

    public Doctor GetDoctor(int doctorID)
    {
        var request = _db.Doctor.FirstOrDefault(u => u.Id == doctorID);

        return new Doctor{
            Id = request.Id,
            Name = request.Name,
            Specialization = request.Specialization
        };
    }

    public List<Doctor> GetDoctors(Specialization specialization)
    {
        var request = _db.Doctor.FirstOrDefault(u => u.Specialization == specialization);

        return new List<Doctor>(new Doctor {
            Id = request.Id,
            Name = request.Name,
            Specialization = request.Specialization
        });
    }
}