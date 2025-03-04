Console.WriteLine((int)Day.Selasa);
Console.WriteLine((Day)2);
Console.WriteLine((Day)7);
Console.WriteLine((Day)0);
Console.WriteLine((Day)11);

foreach (Enum value in Enum.GetValues(typeof(Day)))
{
    Console.WriteLine(value);
}
foreach (string value in Enum.GetNames(typeof(Day)))
{
    Console.WriteLine(value);
}

bool success = Enum.TryParse("senin", out Day side);
Console.WriteLine(success ? side.ToString() : "Parsing failed");


Type integralType = Enum.GetUnderlyingType(Day.Jumat.GetType());
object convertedType = Convert.ChangeType(Day.Jumat, integralType);
Console.WriteLine(Day.Jumat.GetType());
Console.WriteLine(integralType);
Console.WriteLine(convertedType);
Console.WriteLine(convertedType.GetType());


Console.WriteLine(Day.Sabtu.ToString("D"));
Console.WriteLine(Day.Sabtu.ToString("X"));

Day weekend = Day.Sabtu & Day.Minggu;
Console.WriteLine(weekend.ToString("F"));


Enum.TryParse("Rabu", out Day day);
Console.WriteLine(day);


[Flags]
enum Day
{
    Senin = 2,
    Selasa,
    Rabu,
    Kamis,
    Jumat = 9,
    Sabtu,
    Minggu,
    Weekend = Sabtu & Minggu
}