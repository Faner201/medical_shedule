using System.ComponentModel.DataAnnotations;
namespace Database;

public class ScheduleModel
{
    public int Id { get; set; }
    [Required]
    public DoctorModel IdDoctor { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
}