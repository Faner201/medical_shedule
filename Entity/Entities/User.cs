public class User 
{
    private int _id;
    private string _phoneNumber;
    private string _name;
    private AccountRole _role;
    private string _password;
    private string _login;
    public int Id { get; set; }
    public AccountRole Role { get; set; }
    public string Name { get; set; }
    public string PhoneNumber { get; set; }
    public string Password { get; set; }
    public string Login { get; set; }

    public User(int id, string phoneNumber, string name, 
    AccountRole role, string password, string login)
    {
        Id = id;
        PhoneNumber = phoneNumber;
        Name = name;
        Role = role;
        Password = password;
        Login = login;
    }
}
