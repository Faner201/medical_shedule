using System.ComponentModel.DataAnnotations;
namespace Database;

public class ReceptionModel
{
    public int Id { get; set; }
    [Required]
    public DateTime Begin { get; set; }
    public DateTime End { get; set; }
    public UserModel IdUser { get; set; }
    public DoctorModel IdDoctor { get; set; }
    public Specialization SpecializationDoctor { get; set; }
}