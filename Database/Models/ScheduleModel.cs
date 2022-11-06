using System.ComponentModel.DataAnnotations;
namespace Database;

public class ScheduleModel
{
    [Required]
    public int IdDoctor { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
}