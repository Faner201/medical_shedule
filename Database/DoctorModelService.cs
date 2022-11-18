namespace Database;

using Entity;

public class DoctorModelService : IDoctorRepository
{
    private ApplicationContext _db;

    public DoctorModelService(ApplicationContext db)
    {
        _db = db;
    }

    public Doctor? CreateNewDoctor(Doctor doctor)
    {
        var doctor = _db.Doctor.FirstOrDefault(d => d.Name == doctor.Name && d.Specialization == doctor.Specialization);

        if (doctor is null)
            return null;

        _db.Doctor.Add(new DoctorModel 
            {
                Name = doctor.Name,
                Specialization = doctor.Specialization
            }
        );

        _db.SaveChanges();

        return new Doctor(
            doctor.Id,
            doctor.Name,
            doctor.Specialization
        );
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

    public List<Doctor>? GetDoctorList()
    {
        var doctors = from d in Doctor select d;

        if (doctors.Count == 0)
            return new List<Doctor>();

        var doctorsList = doctors.Select(
            d => new Doctor{
                Id = d.Id,
                Name = d.Name,
                Specialization = d.Specialization
            }
        ).ToList();

        return new List<Doctor>(
            doctorsList.Id,
            doctorsList.Name,
            doctorsList.Specialization
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
            doctor.Specialization
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
                Specialization = d.Specialization
            }
        ).ToList();

        return new List<Doctor>(
            doctorsList.Id,
            doctorsList.Name,
            doctorsList.Specialization
        );
    }
}