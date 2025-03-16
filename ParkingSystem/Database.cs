namespace ParkingSystem;
internal static class Database {
    public static List<Vehicle> Vehicles{get;}
    static Database() {
        Vehicles = new();
    }
}