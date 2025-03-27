namespace ParkingSystemLibrary.Models;
public class Karcis {
    public Guid Id {get;} = Guid.NewGuid();
    public Vehicle Vehicle {get;}
    public DateTime EnterTime {get;} = DateTime.Now;
    public DateTime? ExitTime {get; private set;}

    public Karcis(Vehicle vehicle) {
        Vehicle = vehicle;
    }

    internal void EndParking() {
        ExitTime = DateTime.Now.AddHours(Random.Shared.Next(1,13));
    }

    internal TimeSpan GetDuration() {
        return (ExitTime ?? DateTime.Now) - EnterTime;
    }

    internal double CalculateFee() {
        double duration = GetDuration().TotalHours;
        return double.Round(duration * HourlyRate[Vehicle.Type], 2);
    }

    Dictionary<VehicleType, int> HourlyRate = new() {
        {VehicleType.Car, 2_000},
        {VehicleType.Motorcycle, 1_000}
    };
}