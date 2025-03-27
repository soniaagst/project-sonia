using System.ComponentModel.DataAnnotations;

namespace ParkingSystemLibrary.Models;

public class User
{
    [Key]
    public Guid Id {get; set;} = Guid.NewGuid();
    public string Username {get; set;}
    public string Password {get; set;}
    public UserRole Role {get; set;}
}

public enum UserRole
{
    Admin, User
}