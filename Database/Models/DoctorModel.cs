using System.ComponentModel.DataAnnotations;
namespace Database;

public class DoctorModel
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    public Specialization Specialization { get; set; }
    public Doctor(string name, Specialization specialization)
    {
        Name = name;
        Specialization = specialization;
    }
}