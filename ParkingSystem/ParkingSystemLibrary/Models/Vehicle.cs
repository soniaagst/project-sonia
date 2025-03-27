using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ParkingSystemLibrary.Models;
public class Vehicle {
    [Key]
    public string LicensePlate {get; set;}
    public VehicleType Type {get; set;}
    public string Owner {get; set;}

    public override string ToString()
    {
        return $"{Type} - {LicensePlate} - {Owner}";
    }
}

public enum VehicleType {Car, Motorcycle}