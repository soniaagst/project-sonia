ParkingLot parkingLot = new(2, 2);

Vehicle motorHana = new(VehicleType.Bike, "D 5687 XB", "Hana");
Console.WriteLine(motorHana.Info());

Vehicle motorGading = new(VehicleType.Bike, "L 1234 AB", "Gading");
Vehicle motorNadchung = new(VehicleType.Bike, "B 4321 CD", "Nadchung");
Vehicle mobilWulan = new(VehicleType.Car, "S 1234 EF", "Wulan");

Karcis? karcisHana = parkingLot.ParkVehicle(motorHana);
Karcis? karcisGading = parkingLot.ParkVehicle(motorGading);
Karcis? karcisWulan = parkingLot.ParkVehicle(mobilWulan);
Karcis? karcisNadchung = parkingLot.ParkVehicle(motorNadchung);

if (karcisHana != null) parkingLot.RemoveVehicle(motorHana, karcisHana);

parkingLot.ParkVehicle(motorNadchung);

if (karcisGading != null) parkingLot.RemoveVehicle(mobilWulan, karcisGading);