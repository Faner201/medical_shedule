using Entity;
using Database;
using Microsoft.EntityFrameworkCore;
namespace XUnit;

public class DoctorModelUnitTest
{
    private readonly DbContextOptionsBuilder<ApplicationContext> _optionsBuilder;
    private readonly IDoctorRepository _doctorRepository;

    public DoctorModelUnitTest()
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
        optionsBuilder.UseNpgsql(
            "Host=localhost;Port=5432;Database=Test_medical_shedule;Username=postgres;Password=Bkmz2309865"
        );
        _optionsBuilder = optionsBuilder;
        using var _context = new ApplicationContext(_optionsBuilder.Options);
        _doctorRepository = new DoctorModelService(_context);
    }

    [Fact]
    public void CreateNewDoctor()
    {
       string name = "Bebra";
       int specializationId = 5;
       string specializationName = "polka";
       Specialization specialization = new Specialization(specializationId, specializationName);

       var doctor = new Doctor(1, name, specialization);

       var res = _doctorRepository.CreateNewDoctor(name, specialization);

       Assert.True(res is not null);
       Assert.Equal(doctor.Id, res.Id);
       Assert.Equal(doctor.Name, res.Name);
       Assert.Equal(doctor.Specialization.Id, res.Specialization.Id);
       Assert.Equal(doctor.Specialization.Name, res.Specialization.Name);

    }

    [Fact]
    public void DeleteDoctor()
    {
        int doctorId = 1;

        var res = _doctorRepository.DeleteDoctor(doctorId);

        Assert.True(res.Success);
    }

    [Fact]
    public void GetDoctorList()
    {
        string name = "Bebra";
        int specializationId = 5;
        string specializationName = "polka";
        Specialization specialization = new Specialization(specializationId, specializationName);

        var doctor = new Doctor(1, name, specialization);

        var res = _doctorRepository.GetDoctorList();

        Assert.Equal(doctor.Id, res.Id);
        Assert.Equal(doctor.Name, res.Name);
        Assert.Equal(doctor.Specialization.Id, res.Specialization.Id);
        Assert.Equal(doctor.Specialization.Name, res.Specialization.Name);
    }

    [Fact]
    public void GetDoctor()
    {
        string name = "Bebra";
        int specializationId = 5;
        string specializationName = "polka";
        int doctorId = 1;
        Specialization specialization = new Specialization(specializationId, specializationName);

        var doctor = new Doctor(doctorId, name, specialization);

        var res = _doctorRepository.GetDoctor(doctorId);

        Assert.True(res is not null);
        Assert.Equal(doctor.Id, res.Id);
        Assert.Equal(doctor.Name, res.Name);
        Assert.Equal(doctor.Specialization.Id, res.Specialization.Id);
        Assert.Equal(doctor.Specialization.Name, res.Specialization.Name);
    }

    [Fact]
    public void GetDoctors()
    {
        string name = "Bebra";
        int specializationId = 5;
        string specializationName = "polka";
        int doctorId = 1;
        Specialization specialization = new Specialization(specializationId, specializationName);

        var doctor = new Doctor(doctorId, name, specialization);

        var res = _doctorRepository.GetDoctors(specialization);

        Assert.True(res.Count != 0);
        Assert.Equal(doctor.Id, res.Id);
        Assert.Equal(doctor.Name, res.Name);
        Assert.Equal(doctor.Specialization.Id, res.Specialization.Id);
        Assert.Equal(doctor.Specialization.Name, res.Specialization.Name);
    }
}