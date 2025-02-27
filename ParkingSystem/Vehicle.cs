public class Vehicle {
    public VehicleType Type {get;}
    public string LicensePlate {get;}
    public string Owner {get;}

    public Vehicle(VehicleType type, string licensePlate, string owner) {
        Type = type;
        LicensePlate = licensePlate;
        Owner = owner;
    }

    public string Info() {
        return $"{Type} - {LicensePlate}";
    }
}

public enum VehicleType {Car, Bike}