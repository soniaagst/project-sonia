using ParkingSystemLibrary.Models;
using NUnit.Framework.Internal;
using Microsoft.EntityFrameworkCore;

namespace ParkingSystemLibrary.Tests;

[TestFixture]

public class AntarmukaTests
{
    private ParkingLot _parkingLot;
    private VehicleDb _dbContext;
    private ParkingSystemAntarmuka _antarmuka;

    [SetUp]
    public void Setup()
    {
        _parkingLot = new(1,1);

        var options = new DbContextOptionsBuilder<VehicleDb>()
            .UseInMemoryDatabase(databaseName: "TestDb")
            .Options;

        _dbContext = new(options);
        _dbContext.Database.EnsureCreated();

        _antarmuka = new(_parkingLot, _dbContext);
    }

    [TearDown]
    public void Teardown()
    {
        _dbContext.Database.EnsureDeleted();
        _dbContext.Dispose();
    }

    [Test]
    public async Task RegisterVehicle_Should_AddVehicleToDatabase()
    {
        var vehicles = new List<Vehicle>
        {
            new Vehicle(VehicleType.Motorcycle, "A1234B", "Anton"),
            new Vehicle(VehicleType.Motorcycle, "B1234C", "Bagus"),
            new Vehicle(VehicleType.Car, "C1234D", "Candra"),
            new Vehicle(VehicleType.Car, "D1234E", "Dadang")
        };

        foreach (var vhc in vehicles)
        {
            await _antarmuka.RegisterVehicle(vhc.Type, vhc.LicensePlate, vhc.Owner);
        }

        var result = await _antarmuka.GetVehiclesData();

        Assert.That(result, Has.Count.EqualTo(3));
    }

    [Test]
    public async Task ParkVehicle_InputLicensePlateString_ReturnKarcis()
    {
        var karcis = await _antarmuka.ParkVehicle("A1234B");
        Assert.That(karcis, Is.Not.Null);
        Assert.That(karcis, Is.InstanceOf<Karcis>());
    }
}