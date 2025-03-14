Console.WriteLine("Start of foobar");
FooBar.RunVer3(88);

Console.WriteLine("\n\nStart of foobarjazz");
List<object> foobarjazz = FooBar.Foobarjazz2(105).ToList();
foreach (object item in foobarjazz) {
    Console.Write(item);
    if (foobarjazz.IndexOf(item) < foobarjazz.Count - 1) {
        Console.Write(", ");
    }
}

Console.WriteLine("\n\nStart of foobazbarjazzhuzz");
FooBar.Bazhuzz(1260);