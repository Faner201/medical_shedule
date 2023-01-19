using System.ComponentModel.DataAnnotations;
namespace Database;

public class ReceptionModel
{
    public int Id { get; set; }
    [Required]
    public DateTime Begin { get; set; }
    public DateTime End { get; set; }
    public int IdUser { get; set; }
    public int IdDoctor { get; set; }
    public SpecializationModel SpecializationDoctor { get; set; }
}