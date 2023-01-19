using Entity;
using Database;
using Microsoft.EntityFrameworkCore;
namespace XUnit;

public class ReceptionModelUnitTest
{
    private readonly DbContextOptionsBuilder<ApplicationContext> _optionsBuilder;
    private readonly IReceptionRepository _receptionRepository;

    public ReceptionModelUnitTest()
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
        optionsBuilder.UseNpgsql(
            "Host=localhost;Port=5432;Database=Test_medical_shedule;Username=postgres;Password=Bkmz2309865"
        );
        _optionsBuilder = optionsBuilder;
        using var _context = new ApplicationContext(_optionsBuilder.Options);
        _receptionRepository = new ReceptionModelService(_context);
    }
    
    [Fact]
    public void RecordCreation()
    {
       int doctorId = 1;
       int userId = 1;
       var start = new DateTime(1,1,1,1,1,1);
       var end = new DateTime(1, 1, 1, 1, 2, 2);
       int specializationId = 1;
       string specializationName = "aboba";
       Specialization specialization = new Specialization(specializationId, specializationName);

       var record = new Reception(doctorId, userId, start, end, specialization);

       var res = _receptionRepository.RecordCreation(record);

       Assert.True(record is not null);
       Assert.Equal(record.IdDoctor, res.IdDoctor);
       Assert.Equal(record.IdUser, res.IdUser);
       Assert.Equal(record.Start, res.Start);
       Assert.Equal(record.End, res.End);
       Assert.Equal(record.SpecializationDoctor.Id, res.SpecializationDoctor.Id);
       Assert.Equal(record.SpecializationDoctor.Name, res.SpecializationDoctor.Name);
    }

    [Fact]
    public void RecordExists()
    {
        int doctorId = 1;
        int userId = 1;
        var start = new DateTime(1, 1, 1, 1, 1, 1);
        var end = new DateTime(1, 1, 1, 1, 2, 2);
        int specializationId = 1;
        string specializationName = "aboba";
        Specialization specialization = new Specialization(specializationId, specializationName);

        var record = new Reception(doctorId, userId, start, end, specialization);

        var res = _receptionRepository.RecordExists(record);

        Assert.True(res);
    }
}