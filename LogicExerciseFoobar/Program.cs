FooBar fooBar = new();

Console.WriteLine("BEFORE CHANGES");
foreach (var item in fooBar.ShowRules()) {
    Console.WriteLine(item);
}

fooBar.AddRule(2, "hey");
fooBar.RemoveRule(4);
fooBar.EditRule(3, "jude");

Console.WriteLine("AFTER CHANGES");
foreach (var item in fooBar.ShowRules()) {
    Console.WriteLine(item);
}

Console.WriteLine("RUN");
fooBar.Run();