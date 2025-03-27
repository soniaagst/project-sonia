using ParkingSystemLibrary.Models;

namespace ParkingSystemAPI.DTOs;

public class UserDto
{
    public string Username {get; set;}
    public UserRole Role {get; set;}
}