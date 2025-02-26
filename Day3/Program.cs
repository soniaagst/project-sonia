Smartwatch mySmartwatch = new Smartwatch("Samsung", StrapMaterials.Leather);
mySmartwatch.Time();
mySmartwatch.Call("081234567890");
mySmartwatch.Message();

Smartwatch yourSmartwatch = new Smartwatch("Casio");
Console.WriteLine($"Your watch strap made of {yourSmartwatch.StrapMaterial}.");