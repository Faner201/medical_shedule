using System.ComponentModel.DataAnnotations;
namespace Database;

public class AccountRoleModel
{
    public int Id { get; set; }
    [Required]
    public int AccountRoleId { get; set; }
}