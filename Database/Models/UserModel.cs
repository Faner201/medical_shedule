using System.ComponentModel.DataAnnotations;
namespace Database;

public class UserModel
{
    public int Id { get; set; }
    [Required]
    public AccountRoleModel Role { get; set; }
    public string Name { get; set; }
    public string PhoneNumber { get; set; }
    public string Password { get; set; }
    public string Login { get; set; }
}