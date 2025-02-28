public delegate void GreetingTimeHandler(int time);
public delegate void AnotherHandler();

public class Broadcaster {
    public event GreetingTimeHandler? GreetingTime;
    public void Greet() {
        GreetingTime?.Invoke(DateTime.Now.Hour);
    }

    public event AnotherHandler? EventHappened;
    public void InvokeAMethod() {
        EventHappened?.Invoke();
    }
}

public class Subscriber {
    public void OntheTimeoftheDay(int currentTime) {
        if (currentTime < 12) {
            Console.WriteLine("Good Morning!");
        }
        else Console.WriteLine("Good Evening!");
    }

    public void AMethod() {
        Console.WriteLine("Method invoked.");
    }
}
