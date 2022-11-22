using System.ComponentModel.DataAnnotations;
namespace Database;

public class DoctorModel
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    public SpecializationModel Specialization { get; set; }
}