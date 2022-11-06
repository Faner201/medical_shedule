using System.ComponentModel.DataAnnotations;
namespace Database;

public class ReceptionModel
{
    [Required]
    public DateTime Begin { get; set; }
    public DateTime End { get; set; }
    public int IdUser { get; }
    public int IdDoctor { get; }
    public Reception(DateTime begin, DateTime end, int idUser, int idDoctor)
    {
        Begin = begin;
        End = end;
        IdUser = idUser;
        IdDoctor = idDoctor;
    }
}