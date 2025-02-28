public class Slot {
    public int SlotNumber {get;}
    public VehicleType AllowedType {get;}
    public Vehicle? ParkedVehicle {get; private set;}
    public bool IsOccupied {get; private set;}

    public Slot(int slotNumber, VehicleType allowedType) {
        SlotNumber = slotNumber;
        AllowedType = allowedType;
        IsOccupied = false;
    }
    
    public void ParkVehicle(Vehicle vehicle) {
        if (IsOccupied || vehicle.Type != AllowedType) {
            Console.WriteLine("Cannot park here.");
        }
        else {
            ParkedVehicle = vehicle;
            IsOccupied = true;
        }
    }

    public void RemoveVehicle() {
        if (IsOccupied == false) {
            Console.WriteLine("No vehicle to remove.");
        }
        else {
            ParkedVehicle = null;
            IsOccupied = false;
        }
    }
}