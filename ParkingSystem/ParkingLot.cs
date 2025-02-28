public class ParkingLot {
    private List<Slot> Slots = new();

    public ParkingLot(int carSlots, int bikeSlots) {
        int slotNumber = 1;
        for (int i = 0; i < carSlots; i++) {
            Slots.Add(new Slot(slotNumber, VehicleType.Car));
            slotNumber++;
        }
        for (int i = 0; i < bikeSlots; i++) {
            Slots.Add(new Slot(slotNumber, VehicleType.Bike));
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
            Console.WriteLine($"Vehicle {vehicle.LicensePlate} parked at slot #{freeSpace.SlotNumber}. You got a parking ticket!");
            return karcis;
        }
    }

    public double? RemoveVehicle(Vehicle vehicle, Karcis karcis) {
        Slot? occupiedSlot = Slots.Find(slot => slot.ParkedVehicle == vehicle);
        bool isLicenceMatch = vehicle.LicensePlate == karcis.Vehicle.LicensePlate;
        if (occupiedSlot == null || isLicenceMatch == false) {
            Console.WriteLine("Vehicle not found or the ticket doesn't match the vehicle.");
            return null;
        }
        else {
            occupiedSlot?.RemoveVehicle();
            Console.WriteLine($"Vehicle {vehicle.LicensePlate} exited.");
            karcis.EndParking();
            Console.WriteLine($"Enter time: {karcis.EnterTime}");
            Console.WriteLine($"Exit time: {karcis.ExitTime}");
            double fee = karcis.CalculateFee();
            Console.WriteLine($"Parking fee: Rp{fee}");
            return fee;
        }
    }
}