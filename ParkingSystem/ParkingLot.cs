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

    public void ParkVehicle() {
        
         // slot dioccupy (harus kosong dulu), mulai karcis
    }

    public void RemoveVehicle() {
        // remove vehicle dari slot, karcis EndParking, calculatefee
    }
}