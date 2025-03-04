for(int i =0; i < 15; i++) {
    Console.Beep();
}
Console.Write("type something: ");
Console.WriteLine(Console.Read()); // Console.Read reads the 1st caracter and returns its ascii value (hence int)
Console.WriteLine(Console.BackgroundColor);

Console.WriteLine(Environment.MachineName);
Console.WriteLine(Environment.TickCount);
Console.WriteLine(Environment.OSVersion);
Console.WriteLine(Environment.UserName);
Console.WriteLine(Environment.GetEnvironmentVariable("PATH"));