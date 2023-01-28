using Microsoft.EntityFrameworkCore;
using Entity;

namespace Database;

public class DoctorModelService : IDoctorRepository
{
    private ApplicationContext _db;

    public DoctorModelService(ApplicationContext db)
    {
        _db = db;
    }

    async public Task<Doctor?> CreateNewDoctor(Doctor doctor)
    {
        var request = await _db.Doctor.FirstOrDefaultAsync(d => d.Name == doctor.Name && d.Specialization.Id == doctor.Specialization.Id &&
            d.Id == doctor.Id && d.Specialization.Name == doctor.Specialization.Name);

        if (request is null)
            return null;

        await _db.Doctor.AddAsync(new DoctorModel 
            {
                Name = doctor.Name,
                Specialization =  new SpecializationModel(){
                    Name = doctor.Specialization.Name
                }
            }
        );

        await _db.SaveChangesAsync();

        return new Doctor(
            request.Id,
            request.Name,
            new Specialization(request.Specialization.Id, request.Specialization.Name)
        );
    }

    async public Task<bool> DeleteDoctor(int doctorID)
    {
        var request = await _db.Doctor.FirstOrDefaultAsync(u => u.Id == doctorID);

        if (request != null)
        {
            _db.Doctor.Remove(request);
            await _db.SaveChangesAsync();
            return true;
        }
        return false;
    }

    async public Task<List<Doctor>?> GetDoctorList()
    {
        var request = await _db.Doctor
            .Select(d => new Doctor(d.Id, d.Name, new Specialization(d.Specialization.Id, d.Specialization.Name)))
            .ToListAsync();

        if (request is null)
            return null;
        
        return request;
    }

    async public Task<Doctor?> GetDoctor(int doctorID)
    {
        var doctor = await _db.Doctor.FirstOrDefaultAsync(d => d.Id == doctorID);

        if (doctor is null)
            return null;

        return new Doctor(
            doctor.Id,
            doctor.Name,
            new Specialization(doctor.Specialization.Id, doctor.Specialization.Name)
        );
    }

    async public Task<List<Doctor>> GetDoctors(Specialization specialization)
    {
        return await _db.Doctor
            .Where(d => d.Specialization.Name == specialization.Name && d.Specialization.Id == specialization.Id)
            .Select(d => new Doctor(d.Id, d.Name, new Specialization(d.Specialization.Id, d.Specialization.Name)))
            .ToListAsync();
    }
}