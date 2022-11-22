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
                Specialization =  new Specialization(doctor.Specialization.Id, doctor.Specialization.Name)
            }
        );

        _db.SaveChanges();

        return new Doctor(
            request.Id,
            request.Name,
            request.Specialization.Id,
            request.Specialization.Name
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
        
        return new List<Doctor>(
            request.Id,
            request.Name,
            request.Specialization.Id,
            request.Specialization.Name
        );
    }

    public Doctor? GetDoctor(int doctorID)
    {
        var doctor = _db.Doctor.FirstOrDefault(d => d.Id == doctorID);

        if (doctor is null)
            return null;

        return new Doctor{
            doctor.Id,
            doctor.Name,
            doctor.Specialization.Id,
            doctor.Specialization.Name
        };
    }

    public List<Doctor> GetDoctors(Specialization specialization)
    {
        var doctors = _db.Doctor.Where(d => d.Specialization == specialization).ToList();

        if (doctors.Count == 0)
            return new List<Doctor>();


        var doctorsList = doctors.Select(
            d => new Doctor{
                Id = d.Id,
                Name = d.Name,
                Specialization = new Specialization(d.Specialization.Id, d.Specialization.Name)
            }
        ).ToList();

        return new List<Doctor>(
            doctorsList.Id,
            doctorsList.Name,
            doctorsList.Specialization.Id,
            doctorsList.Specialization.Name
        );
    }
}