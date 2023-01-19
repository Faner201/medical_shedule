using System.Text.Json.Serialization;
using Entity;

namespace Api;

public class UserView
{
    [JsonPropertyName("id")]
    public int ID { get; set; }
    [JsonPropertyName("name")]
    public string Name { get; set; } = "";
    [JsonPropertyName("phone")]
    public string PhoneNumber { get; set; } = "";
    [JsonPropertyName("role")]
    public AccountRole Role { get; }
    [JsonPropertyName("login")]
    public string Login { get; set; } = "";
    [JsonPropertyName("password")]
    public string Password { get; set; } = "";

    public UserView(User user) {
        ID = user.ID;
        PhoneNumber = user.PhoneNumber;
        Name = user.Name;
        Role = user.Role;
        Login = user.Login;
        Password = user.Password;
    }
}