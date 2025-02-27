public class Karcis {
    public Vehicle Vehicle {get;}
    public DateTime EnterTime {get;}
    public DateTime? ExitTime {get; private set;}

    public Karcis(Vehicle vehicle) {
        Vehicle = vehicle;
        EnterTime =  DateTime.Now;
    }

    public void EndParking() {
        ExitTime = DateTime.Now.AddHours(Random.Shared.Next(1,13));
    }

    public TimeSpan GetDuration() {
        return (ExitTime ?? DateTime.Now) - EnterTime;
    }

    public double CalculateFee() {
        double duration = GetDuration().TotalHours;
        return double.Round(duration * HourlyRate[Vehicle.Type], 2);
    }

    Dictionary<VehicleType, int> HourlyRate = new() {
        {VehicleType.Car, 2_000},
        {VehicleType.Bike, 1_000}
    };
}