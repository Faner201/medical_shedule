using Microsoft.EntityFrameworkCore;
using Entity;

namespace Database;

public class ReceptionModelService : IReceptionRepository
{
    private ApplicationContext _db;

    public ReceptionModelService(ApplicationContext db)
    {
        _db = db;
    }

    async public Task<Reception?> RecordCreation(Reception reception)
    {
        var request = await _db.Reception.FirstOrDefaultAsync(rp => rp.IdDoctor == reception.IdDoctor &&
            rp.IdUser == reception.IdUser && rp.Begin == reception.Start && rp.End == reception.End &&
            rp.SpecializationDoctor.Id == reception.SpecializationDoctor.Id && reception.SpecializationDoctor.Name == reception.SpecializationDoctor.Name);

        if (request is not null)
            return null;

        await _db.Reception.AddAsync(new ReceptionModel
            { 
                IdDoctor = reception.IdDoctor,
                IdUser = reception.IdUser,
                Begin = reception.Start,
                End = reception.End,
                SpecializationDoctor = new SpecializationModel(){
                    Name = reception.SpecializationDoctor.Name
                }
            }
        );
        await _db.SaveChangesAsync();

        return new Reception(
            request.IdDoctor,
            request.IdUser,
            request.Begin,
            request.End,
            new Specialization(reception.SpecializationDoctor.Id,reception.SpecializationDoctor.Name)
        );
    }

    async public Task<bool> RecordExists(Reception reception)
    {
        var request = await _db.Reception.FirstOrDefaultAsync(rp => rp.IdDoctor == reception.IdDoctor &&
            rp.IdUser == reception.IdUser && rp.Begin == reception.Start && rp.End == reception.End &&
            rp.SpecializationDoctor.Id == reception.SpecializationDoctor.Id && reception.SpecializationDoctor.Name == reception.SpecializationDoctor.Name);

        if (request is null)
            return false;

        return true;
    }

    async public Task<List<(DateTime, DateTime)>> GetAllDates(Specialization specialization, DateOnly date)
    {
        var dateTime = date.ToDateTime(new TimeOnly(0, 0, 0));
        return await _db.Reception
            .Where(rp => rp.SpecializationDoctor.Name == specialization.Name && rp.Begin.Date == dateTime)
            .Select(rp => new Tuple<DateTime, DateTime>(rp.Begin, rp.End).ToValueTuple())
            .OrderBy(rp => rp.Item2).ToListAsync();
    }
}