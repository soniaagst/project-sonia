using ParkingSystemLibrary.Models;
using NUnit.Framework.Internal;

namespace ParkingSystemLibrary.Tests;

[TestFixture]
public class ParkingLotTests
{
    private ParkingLot _parkingLot;

    [SetUp]
    public void Setup()
    {
        _parkingLot = new(1,1);
    }

    private static IEnumerable<Vehicle> VehicleTestCases()
    {
        yield return  new Vehicle(VehicleType.Motorcycle, "A1234B", "Anton");
        yield return  new Vehicle(VehicleType.Motorcycle, "B1234C", "Bagus");
        yield return  new Vehicle(VehicleType.Car, "C1234D", "Candra");
        yield return  new Vehicle(VehicleType.Car, "D1234E", "Dadang");
    }

    [Test, TestCaseSource(nameof(VehicleTestCases))]
    public void ParkVehicle_InputVehicle_ReturnKarcis(Vehicle vehicle)
    {
        var karcis = _parkingLot.ParkVehicle(vehicle);
        Assert.That(karcis, Is.InstanceOf<Karcis>());
    }
}