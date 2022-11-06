using System.ComponentModel.DataAnnotations;
namespace Database;

public class AccountRoleModel
{
    private int _id;
    [Required]
    public AccountRole(int id)
    {
        _id = id;
    }
}