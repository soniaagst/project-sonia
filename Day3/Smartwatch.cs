internal class Smartwatch : IWatch, ISmartphone {
    public string Brand {get; set;}
    public StrapMaterials StrapMaterial {get; set;}

    public Smartwatch(string brand) {
        Brand = brand;
        StrapMaterial = StrapMaterials.Rubber;
    }

    public Smartwatch(string brand, StrapMaterials strapType) {
        Brand = brand;
        StrapMaterial = strapType;
    }

    public DateTime Time() {
        return DateTime.Now;
    }
    
    public void SettingAlarm() {}
    public void RingingAlarm() {}

    public void Call(string destinationNumber) {
        Console.WriteLine($"Calling {destinationNumber}...");
        Console.WriteLine("Press any to end call.");
        Console.ReadKey();
        Console.WriteLine("Call ended.");
    }
    public void Message() {
        Console.Write("To: ");
        string? destinationNumber = Console.ReadLine();
        while (string.IsNullOrEmpty(destinationNumber)) {
            Console.Write("Number field cannot empty. \n To: ");
            destinationNumber = Console.ReadLine();
        }
        Console.WriteLine("Message:");
        string? messageContent = Console.ReadLine();
        Console.WriteLine("Message sent!");
    }
    public void OpenMessage(string messageID) {
        // show message function
    }
}

public interface IWatch {
    DateTime Time();
    StrapMaterials StrapMaterial {get; set;}
    void SettingAlarm();
    void RingingAlarm();
}

public interface ISmartphone {
    void Call(string destinationNumber);
    void Message();
    void OpenMessage(string messageID);
}

public enum StrapMaterials {
    Rubber,
    Leather,
    Metal
}
