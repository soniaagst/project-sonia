public class Vehicle {
    public VehicleType Type {get;}
    public string LicensePlate {get;}
    public Vehicle(VehicleType type, string licensePlate) {
        Type = type;
        LicensePlate = licensePlate;
    }

    public string Info() {
        return $"{Type} - {LicensePlate}";
    }
}

public enum VehicleType {Car, Bike}