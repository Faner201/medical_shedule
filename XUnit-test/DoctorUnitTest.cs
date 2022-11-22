using Xunit;
using Entity;

namespace XUnit;

public class DoctorUnitTest
{
    private readonly DoctorService _doctorService;
    private readonly Mock<IDoctorRepository> _doctorRepositoryMock;

    public DoctorUnitTest()
    {
        _doctorRepositoryMock = new Mock<IDoctorRepository>();
        _doctorService = new DoctorService(_doctorRepositoryMock.Object);
    }

    [Fact]
    public void CreateNewDoctorWithEmptyName_ShouldFail()
    {
        var res = _doctorService.CreateNewDoctor(new Doctor(default, "", default));
        var received = res.Error;

        Assert.True(res.IsFail);
        Assert.Equal("Nonexistent doctor name", received);
    }

    [Fact]
    public void CreateNewDoctorWithEmptySpecialization_ShouldFail()
    {
        var res = _doctorService.CreateNewDoctor(new Doctor(default, "faner201", default));
        var received = res.Error;

        Assert.True(res.IsFail);
        Assert.Equal("Doctor without a specialty", received);
    }

    [Fact]
    public void CreateNewDoctorSuccessfully_ShouldOk()
    {
        _doctorRepositoryMock.Setup(repository => repository.CreateNewDoctor(It.IsAny<Doctor>()))
            .Returns(() => new Doctor(0, "faner201", new Specialization(0, "faner")));

        var res = _doctorService.CreateNewDoctor(new Doctor(1, "fkdsf", new Specialization(0, "fwefwe")));
        var received = res.Error;

        Assert.True(res.Success);
        Assert.Equal(string.Empty, received);
    }

    [Fact]
    public void CreateNewDoctorAnotherErorr_ShouldFail()
    {
        _doctorRepositoryMock.Setup(repository => repository.CreateNewDoctor(new Doctor(0, "faner201", new Specialization(0, "faner"))))
            .Returns(() => null);

        var res = _doctorService.CreateNewDoctor(new Doctor(1, "fkdsf", new Specialization(0, "fwefwe")));
        var received = res.Error;

        Assert.True(res.IsFail);
        Assert.Equal("Error when creating a doctor", received);
    }

    [Fact]
    public void DeleteDoctorWithEmptyDoctorID_ShouldFail()
    {
        int id = 1;
        _doctorRepositoryMock.Setup(repository => repository.GetDoctor(It.IsAny<int>()))
            .Returns(() => null);

        var res = _doctorService.DeleteDoctor(id);
        var received = res.Error;

        Assert.True(res.IsFail);
        Assert.Equal("Doctor not found", received);
    }

    [Fact]
    public void DeleteDoctorSuccessfully_ShouldOk()
    {
        int id = 1;
        _doctorRepositoryMock.Setup(repository => repository.GetDoctor(1))
            .Returns(() => new Doctor(id, "faner201", default));
        
        _doctorRepositoryMock.Setup(repository => repository.DeleteDoctor(It.IsAny<int>()))
            .Returns(() => true);

        var res = _doctorService.DeleteDoctor(id);
        var received = res.Error;

        Assert.True(res.Success);
        Assert.Equal(string.Empty, received);
    }

    [Fact]
    public void DeleteDoctorAnotherErorr_ShouldFail()
    {
        int id = 1;
        _doctorRepositoryMock.Setup(repository => repository.GetDoctor(1))
            .Returns(() => new Doctor(id, "faner201", default));
        
        _doctorRepositoryMock.Setup(repository => repository.DeleteDoctor(It.IsAny<int>()))
            .Returns(() => false);

        var res = _doctorService.DeleteDoctor(id);
        var received = res.Error;

        Assert.True(res.IsFail);
        Assert.Equal("Doctor not deleted", received);
    }

    [Fact]
    public void GetDoctorListWithEmptyList_ShouldFail()
    {
        _doctorRepositoryMock.Setup(repository => repository.GetDoctorList())
            .Returns(() => null);
        
        var res = _doctorService.GetDoctorList();
        var received = res.Error;

        Assert.True(res.IsFail);
        Assert.Equal("The list of doctors was empty", received);
    }

    [Fact]
    public void GetDoctorListSuccessfully_ShouldOk()
    {
        _doctorRepositoryMock.Setup(repository => repository.GetDoctorList())
            .Returns(() => new List<Doctor>());

        var res = _doctorService.GetDoctorList();
        var received = res.Error;

        Assert.True(res.Success);
        Assert.Equal(string.Empty, received);
    }

    [Fact]
    public void GetDoctorWithEmpryDoctorID_ShouldFail()
    {
        int id = 1;
        _doctorRepositoryMock.Setup(repository => repository.GetDoctor(It.IsAny<int>()))
            .Returns(() => null);
        
        var res = _doctorService.GetDoctor(id);
        var received = res.Error;

        Assert.True(res.IsFail);
        Assert.Equal("Doctor not found", received);
    }

    [Fact]
    public void GetDoctorSuccessfully_ShouldOk()
    {
        int id = 1;
        _doctorRepositoryMock.Setup(repository => repository.GetDoctor(It.IsAny<int>()))
            .Returns(() => new Doctor(id, "", default));
        
        var res = _doctorService.GetDoctor(id);
        var received = res.Value.Id;

        Assert.True(res.Success);
        Assert.Equal(id, received);
    }

    [Fact]
    public void GetDoctorsWithEmptySpecialization_ShouldFail()
    {
        var res = _doctorService.GetDoctors(new Specialization(default, ""));
        var received = res.Error;

        Assert.True(res.IsFail);
        Assert.Equal("No existing specialization", received);
    }

    [Fact]
    public void GetDoctorsWithEmptyList_ShouldFail()
    {
        _doctorRepositoryMock.Setup(repository => repository.GetDoctors(It.IsAny<Specialization>()))
            .Returns(() => null);
        
        var res = _doctorService.GetDoctors(new Specialization(default, "faner201"));
        var received = res.Error;

        Assert.True(res.IsFail);
        Assert.Equal("Doctors not found", received);
    }

    [Fact]
    public void GetDoctorsSuccessfully_ShouldOk()
    {
        _doctorRepositoryMock.Setup(repository => repository.GetDoctors(It.IsAny<Specialization>()))
            .Returns(() => new List<Doctor>());
        
        var res = _doctorService.GetDoctors(new Specialization(default, "faner201"));
        var received = res.Error;

        Assert.True(res.Success);
        Assert.Equal(string.Empty, received);
    }

}