namespace ParkingSystemLibrary.Models;
public sealed class ParkingLot {
    private List<Slot> Slots = new();
    private Dictionary<string, Karcis> validKarcis = new();

    public ParkingLot(int carSlots = 30, int bikeSlots = 800) {
        int slotNumber = 1;
        for (int i = 0; i < carSlots; i++) {
            Slots.Add(new Slot(slotNumber, VehicleType.Car));
            slotNumber++;
        }
        for (int i = 0; i < bikeSlots; i++) {
            Slots.Add(new Slot(slotNumber, VehicleType.Motorcycle));
            slotNumber++;
        }
    }

    public Karcis? ParkVehicle(Vehicle vehicle) {
        Slot? freeSpace = Slots.Find(slot => (slot.IsOccupied == false) && (slot.AllowedType == vehicle.Type));
        if (freeSpace == null) {
            Console.WriteLine($"No free space for {vehicle.Type}.");
            return null;
        }
        else {
            freeSpace.ParkVehicle(vehicle);
            Karcis karcis = new(vehicle);
            validKarcis.Add(vehicle.LicensePlate, karcis);
            Console.WriteLine($"Vehicle {vehicle.LicensePlate} parked at slot #{freeSpace.SlotNumber}. You got a parking ticket!");
            return karcis;
        }
    }

    internal double? RemoveVehicle(Vehicle vehicle, Guid karcisId) {
        Karcis? karcis = validKarcis.Values.FirstOrDefault( k=> k.Id == karcisId);

        if (karcis is not null) {
            bool validated = validKarcis.ContainsKey(vehicle.LicensePlate) && (vehicle.LicensePlate == karcis.Vehicle.LicensePlate);
            Slot? occupiedSlot = Slots.Find(slot => slot.ParkedVehicle?.LicensePlate == vehicle.LicensePlate);

            if (!validated || occupiedSlot == null) {
                Console.WriteLine("Invalid ticket or vehicle not found.");
                return null;
            }
            else {
                occupiedSlot.RemoveVehicle();
                karcis.EndParking();
                validKarcis.Remove(vehicle.LicensePlate);
                double fee = karcis.CalculateFee();

                Console.WriteLine($"Vehicle {vehicle.LicensePlate} exited.");
                Console.WriteLine($"Enter time: {karcis.EnterTime}");
                Console.WriteLine($"Exit time: {karcis.ExitTime}");
                Console.WriteLine($"Parking fee: Rp{fee}");
                return fee;
            }
        }
        return null;
    }
}