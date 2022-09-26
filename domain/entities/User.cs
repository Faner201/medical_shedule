class User {
    private int _id;
    private int _phoneNumber;
    private string _name;
    private AccountRole _role;
    public int Id { get => _id; set => _id = value; }
    internal AccountRole Role { get => _role; set => _role = value; }
    public string Name { get => _name; set => _name = value; }
    public int PhoneNumber { get => _phoneNumber; set => _phoneNumber = value; }
}