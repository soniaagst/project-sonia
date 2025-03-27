using System.ComponentModel.DataAnnotations;

namespace ParkingSystemLibrary.Models;

public class User
{
    [Key]
    public Guid Id {get; set;}
    public string Username {get; set;}
    public string Password {get; set;}
    public UserRole Role {get; set;}

    public User() {}

    public User(string username, string password)
    {
        Id = Guid.NewGuid();
        Username = username;
        Password = password;
    }
}

public enum UserRole
{
    admin
}