namespace ParkingSystem;
public class AntarmukaParkingSys {
    private List<Vehicle> _vehicles;
    private ParkingLot _parkingLot;
    public AntarmukaParkingSys(ParkingLot parkingLot) {
        _vehicles = Database.Vehicles;
        _parkingLot = parkingLot;
    }

    public void RegisterVehicle(VehicleType vehicleType, string licensePlate, string owner) {
        Vehicle vehicle = new (vehicleType, licensePlate, owner);
        _vehicles.Add(vehicle);
    }

    public List<Vehicle> GetVehiclesData() {
        return _vehicles;
    }

    public List<Vehicle> SearchbyOwner(string owner) {
        return _vehicles.Where(vehicle => vehicle.Owner == owner).ToList();
    }
    
    public Vehicle? SearchByLicensePlate(string licensePlate) {
        return _vehicles.FirstOrDefault(vehicle => vehicle.LicensePlate == licensePlate);
    }

    public bool UnregVehicle(string licensePlate) {
        Vehicle? vehicle = SearchByLicensePlate(licensePlate);
        if (vehicle is not null) {
            _vehicles.Remove(vehicle);
            return true;
        }
        else {
            return false;
        }
    }

    public Karcis? ParkVehicle(string licensePlate) {
        Vehicle? vehicle = SearchByLicensePlate(licensePlate);
        if (vehicle is null) return null;
        return  _parkingLot.ParkVehicle(vehicle);
    }

    public double? UnparkVehicle(string licensePlate, string karcisId) {
        Vehicle? vehicle = _vehicles.FirstOrDefault(vehic => vehic.LicensePlate == licensePlate);
        if (vehicle is not null) {
            return _parkingLot.RemoveVehicle(vehicle, karcisId);
        }
        return null;
    }
}