ParkingLot parkingLot = new(2, 2);

Vehicle motorHana = new(VehicleType.Motorcycle, "D 5687 XB", "Hana");
Console.WriteLine(motorHana.ToString());

Vehicle motorGading = new(VehicleType.Motorcycle, "L 1234 AB", "Gading");
Vehicle motorNadchung = new(VehicleType.Motorcycle, "B 4321 CD", "Nadchung");
Vehicle mobilWulan = new(VehicleType.Car, "S 1234 EF", "Wulan");

Karcis? karcisHana = parkingLot.ParkVehicle(motorHana);
Karcis? karcisGading = parkingLot.ParkVehicle(motorGading);
Karcis? karcisWulan = parkingLot.ParkVehicle(mobilWulan);
Karcis? karcisNadchung = parkingLot.ParkVehicle(motorNadchung);

parkingLot.RemoveVehicle(motorNadchung, karcisNadchung); // harusnya motornya not found krena Nadchung gagal parkir karena slot motor penuh

if (karcisHana != null) parkingLot.RemoveVehicle(motorHana, karcisHana);

parkingLot.ParkVehicle(motorNadchung); // Nadchung bisa parkir karena Hana keluar

if (karcisGading != null) parkingLot.RemoveVehicle(mobilWulan, karcisGading);