Vehicle motorHana = new(VehicleType.Bike, "D 5687 XB", "hana");
Console.WriteLine(motorHana.Info());
Karcis karcisHana = new(motorHana);
karcisHana.EndParking();
Console.WriteLine(karcisHana.EnterTime);
Console.WriteLine(karcisHana.ExitTime);
Console.WriteLine(karcisHana.GetDuration());
Console.WriteLine(karcisHana.CalculateFee());