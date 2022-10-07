public class User 
{
    private int _id;
    private string _phoneNumber;
    private string _name;
    private AccountRole _role;
    private string _password;
    private string _login;
    public int Id { get => _id; set => _id = value; }
    public AccountRole Role { get => _role; set => _role = value; }
    public string Name { get => _name; set => _name = value; }
    public string PhoneNumber { get => _phoneNumber; set => _phoneNumber = value; }
    public string Password { get => _password; set => _password = value; }
    public string Login { get => _login; set => _login = value; }

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
