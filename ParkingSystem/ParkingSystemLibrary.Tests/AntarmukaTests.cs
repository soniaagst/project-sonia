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
        await _antarmuka.RegisterVehicle(VehicleType.Car, "D1234E", "Dadang");

        var result = await _antarmuka.GetVehiclesData();

        Assert.That(result, Has.Count.EqualTo(1));
    }

    [Test]
    public async Task UnregVehicle_InputLicense_ReturnTrue()
    {
        await _antarmuka.RegisterVehicle(VehicleType.Car, "E1234F", "Endah");

        var result = await _antarmuka.UnregVehicle("E1234F");

        Assert.That(result, Is.True);
    }

    [Test]
    public async Task ParkVehicle_Should_ReturnKarcis()
    {
        var vehicles = new List<Vehicle>
        {
            new Vehicle(VehicleType.Motorcycle, "A1234B", "Anton"),
            new Vehicle(VehicleType.Motorcycle, "B1234C", "Bagus"),
            new Vehicle(VehicleType.Car, "C1234D", "Candra"),
            new Vehicle(VehicleType.Car, "D1234E", "Dadang")
        };

        List<Karcis?> karcis2 = [];

        foreach (var vhc in vehicles)
        {
            await _antarmuka.RegisterVehicle(vhc.Type, vhc.LicensePlate, vhc.Owner);

            karcis2.Add(await _antarmuka.ParkVehicle(vhc.LicensePlate));
        }

        var karcis = await _antarmuka.ParkVehicle("A1111A");

        Assert.That(karcis2[0], Is.Not.Null);
        Assert.That(karcis2[0], Is.InstanceOf<Karcis>());
        Assert.That(karcis2[1], Is.Null);
        Assert.That(karcis, Is.Null);
    }

    [Test]
    public async Task UnparkVehicle_Should_ReturnDouble()
    {
        await _antarmuka.RegisterVehicle(VehicleType.Motorcycle, "F1234G", "Fitri");
        var karcis = await _antarmuka.ParkVehicle("F1234G");
        
        var result = await _antarmuka.UnparkVehicle("F1234G", karcis!.Id);

        Assert.That(result, Is.InstanceOf<double>());
    }
}