using System.ComponentModel.DataAnnotations;
namespace Database;

public class SpecializationModel
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }

    public SpecializationModel(string name)
    {
        Name = name;
    }
}