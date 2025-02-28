Broadcaster broadcaster = new();
Subscriber subscriber = new();
Subscriber subscriber1 = new();

broadcaster.GreetingTime += subscriber.OntheTimeoftheDay;
broadcaster.EventHappened += subscriber1.AMethod;

broadcaster.Greet();
broadcaster.InvokeAMethod();

broadcaster.GreetingTime -= subscriber.OntheTimeoftheDay;
broadcaster.EventHappened -= subscriber1.AMethod;