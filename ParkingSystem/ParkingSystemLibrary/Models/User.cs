namespace ParkingSystemLibrary.Models;

public class User
{
    public Guid Id {get;}
    public string Username {get; set;}
    public string Password {get; set;}
    public UserRole Role {get; set;}

    public User(string username, string password)
    {
        Username = username;
        Password = password;
    }
}

public enum UserRole
{
    admin
}