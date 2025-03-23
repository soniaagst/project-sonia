namespace ParkingSystemLibrary.Models;

internal static class Database {
    public static List<Vehicle> Vehicles{get;}
    static Database() {
        Vehicles = new();
    }
}