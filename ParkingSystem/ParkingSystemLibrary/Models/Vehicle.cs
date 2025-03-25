using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ParkingSystemLibrary.Models;
public class Vehicle {
    [Key]
    public string LicensePlate {get;}
    public VehicleType Type {get;}
    public string Owner {get;}

    private Vehicle() {
        LicensePlate = GetFakeLicensePlate();
        Owner = "unknown";
    }

    public Vehicle(VehicleType type, string licensePlate, string owner) {
        Type = type;
        LicensePlate = licensePlate;
        Owner = owner;
    }

    public override string ToString()
    {
        return $"{Type} - {LicensePlate} - {Owner}";
    }

    private string GetFakeLicensePlate() {
        string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        string numbers = "0123456789";

        StringBuilder sb = new(8);

        Random random = new Random();

        sb.Append(Enumerable.Range(0, 2)
            .Select(_ => letters[random.Next(letters.Length)]).ToArray());

        sb.Append(Enumerable.Range(0, 4)
            .Select(_ => numbers[random.Next(numbers.Length)]).ToArray());
        
        sb.Append(Enumerable.Range(0, 2)
            .Select(_ => letters[random.Next(letters.Length)]).ToArray());

        return sb.ToString();
    }
}

public enum VehicleType {Car, Motorcycle}